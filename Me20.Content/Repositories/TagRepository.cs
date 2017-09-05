using Me20.Contracts;
using Me20.Contracts.Entities;
using Me20.DAL.Graph;
using Me20.Shared.Extensions;
using System;
using System.Threading.Tasks;

namespace Me20.Content.Repositories
{
    public class TagRepository
    {
        private const string TypePropertyConstant = "tag";

        public TagRepository()
        {

        }

        public async Task AddTagVertexAsync(ITag tag)
        {
            try
            {
                using (var client = new GremlinClient())
                {
                    await client.Execute(GremlinQuery.g.addV(tag.TagName, tag.TagNameToId())
                        .property("type", TypePropertyConstant)
                        .property("tagName", tag.TagName));
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

        public async Task AddTagSubscribedByUserEdgeAsync(IHaveTagName tagNameContainer, string userName)
        {
            using (var client = new GremlinClient())
            {
                var query = GremlinQuery.gV(tagNameContainer.TagNameToId())
                    .addE("subscribedBy")
                    .to(GremlinQuery.V(userName))

                    .property("when", DateTime.UtcNow.Ticks);

                await client.Execute(query);
            }
        }
    }
}
