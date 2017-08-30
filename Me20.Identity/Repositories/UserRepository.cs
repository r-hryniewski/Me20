using Me20.Contracts.Entities;
using Me20.DAL.Graph;
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
                    await client.Execute(GremlinQuery.g.addV(user.UserName, user.UserName).property("type", TypePropertyConstant));
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
    }
}
