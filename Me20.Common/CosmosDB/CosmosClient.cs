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

namespace Me20.Common.CosmosDB
{
    public class GremlinCosmosClient : IDisposable
    {
        private static readonly string CosmosDBEndpoint = ConfigurationManager.AppSettings["CosmosDBEndpoint"];
        private static readonly string CosmosDBAuthKey = ConfigurationManager.AppSettings["CosmosDBKey"];
        private static readonly string CosmosDBDatabaseId = ConfigurationManager.AppSettings["CosmosDBDatabaseId"];
        private static readonly string DBDocsCollectionId = ConfigurationManager.AppSettings["CosmosDBCollectionId"];

        private readonly DocumentClient client;
        private Database database;
        private DocumentCollection graph;

        public GremlinCosmosClient()
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
                UriFactory.CreateDatabaseUri(CosmosDBDatabaseId),
                new DocumentCollection { Id = DBDocsCollectionId },
                new RequestOptions { OfferThroughput = 400 }); //TODO: Extract this to some kind of settings

            return graph;
        }

        //TODO: Linq, paging etc
        public async Task<IEnumerable<TResult>> Execute<TResult>(string gremlinQuery, Func<Vertex, TResult> selector, CancellationToken ct)
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
