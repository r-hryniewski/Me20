using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Graphs;
using Microsoft.Azure.Graphs.Elements;
using Microsoft.Azure.Graphs.Common;
using Microsoft.Azure.Graphs.Translator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;

namespace Me20.DAL.Graph
{
    public class GremlinClient : IDisposable
    {
        private static readonly string CosmosDBEndpoint = CloudConfigurationManager.GetSetting("CosmosDBEndpoint");
        private static readonly string CosmosDBAuthKey = CloudConfigurationManager.GetSetting("CosmosDBKey");
        private static readonly string CosmosDBDatabaseId = CloudConfigurationManager.GetSetting("CosmosDBDatabaseId");
        private static readonly string DBDocsCollectionId = CloudConfigurationManager.GetSetting("CosmosDBCollectionId");

        private readonly DocumentClient client;
        private Database database;
        private DocumentCollection graph;

        public GremlinClient()
        {
            client = new DocumentClient(
                new Uri(CosmosDBEndpoint),
                CosmosDBAuthKey,
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp });
        }

        private async Task<DocumentCollection> GetGraph()
        {
            if (database == null)
                database = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = CosmosDBDatabaseId });

            if (graph == null)
                graph = await client.CreateDocumentCollectionIfNotExistsAsync(
                    databaseUri: UriFactory.CreateDatabaseUri(CosmosDBDatabaseId),
                    documentCollection: new DocumentCollection { Id = DBDocsCollectionId },
                    options: new RequestOptions { OfferThroughput = 400 }); //TODO: Extract this to some kind of settings

            return graph;
        }

        //TODO: Linq, paging etc
        public async Task<IEnumerable<TResult>> Execute<TResult>(string gremlinQuery, Func<Vertex, TResult> selector, CancellationToken ct = default(CancellationToken))
        {
            var query = client.CreateGremlinQuery<Vertex>(await GetGraph(), gremlinQuery);
            var results = new List<TResult>();
            while (query.HasMoreResults)
            {
                foreach (var vertex in await query.ExecuteNextAsync<Vertex>())
                {
                    results.Add(selector(vertex));
                }
            }
            return results;
        }

        public async Task Execute(string gremlinQuery, CancellationToken ct = default(CancellationToken))
        {
            var query = client.CreateGremlinQuery<Vertex>(await GetGraph(), gremlinQuery);
            while (query.HasMoreResults)
            {
                await query.ExecuteNextAsync<Vertex>();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                client.Dispose();

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            
            Dispose(true);
        }
        #endregion

    }
}
