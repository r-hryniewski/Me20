﻿using Me20.Contracts.Entities;
using Me20.DAL.Graph;
using Me20.Shared.Extensions;
using System;
using System.Threading.Tasks;
using Me20.Contracts.Commands;
using Me20.Contracts;

namespace Me20.Content.Repositories
{
    public class ContentRepository
    {
        private const string TypePropertyConstant = "content";
        public ContentRepository()
        {
        }

        public async Task AddContentVertexAsync(IContent content)
        {
            try
            {
                using (var client = new GremlinClient())
                {
                    await client.Execute(GremlinQuery.g.addV(content.ContentUri.ToString(), content.Id).property("type", TypePropertyConstant));
                }
            }
            catch (Microsoft.Azure.Documents.DocumentClientException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    //Item already exists and cannot be added second time, do nothing
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddContentAddedByUserEdgeAsync(string userName, Uri contentUri)
        {
            try
            {
                var contentId = contentUri.ToSchemalessUriAsMD5();
                using (var client = new GremlinClient())
                {
                    var query = GremlinQuery.gV(contentId)
                        .addE("addedBy")
                        .to(GremlinQuery.V(userName))

                        .property("when", DateTime.UtcNow.Ticks);

                    await client.Execute(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddContentTaggedWithEdgeAsync(IHaveContentUri contentUriContainer, IHaveTagName tagNameContainer)
        {
            try
            {
                var contentId = contentUriContainer.SchemalessUriAsMD5();
                var tagId = tagNameContainer.TagNameToId();

                using (var client = new GremlinClient())
                {
                    var query = GremlinQuery.gV(contentId)
                        .addE("taggedWith")
                        .to(GremlinQuery.V(tagId))

                        .property("when", DateTime.UtcNow.Ticks); 

                    await client.Execute(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
