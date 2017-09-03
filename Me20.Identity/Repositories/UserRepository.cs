using Me20.Contracts.Entities;
using Me20.DAL.Graph;
using Me20.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Identity.Repositories
{
    public class UserRepository
    {
        private const string TypePropertyConstant = "user";
        public UserRepository()
        {

        }

        public async Task AddAsync(IUser user)
        {
            try
            {
                using (var client = new GremlinClient())
                {
                    var query = GremlinQuery.g.addV(user.UserName, user.UserName)
                        .property("type", TypePropertyConstant)
                        .property("externalId", user.Id)
                        .property("authenticationType", user.AuthenticationType)
                        .property("created", DateTime.UtcNow.Ticks);

                    await client.Execute(query);
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

        public async Task AddContentAddedForUserEdgeAsync(string userName, Uri contentUri)
        {
            try
            {
                var nowTicks = DateTime.UtcNow;
                var contentId = contentUri.ToSchemalessUriAsMD5();
                using (var client = new GremlinClient())
                {
                    var query = GremlinQuery.gV(userName)
                        .addE("added")
                        .to(GremlinQuery.V(contentId))

                        .property("when", DateTime.UtcNow.Ticks); //TODO: Move generating id to IHaveContentUri extensions?

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
