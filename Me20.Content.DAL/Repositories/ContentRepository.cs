using Me20.Contracts.Entities;
using Me20.DAL.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Me20.Content.DAL.Repositories
{
    public class ContentRepository
    {
        private const string LabelPrefix = "content-";
        public ContentRepository()
        {
        }

        public async Task AddAsync(IContent content)
        {
            try
            {
                using (var client = new GremlinClient())
                {
                    await client.Execute(GremlinQuery.g.addV($"{LabelPrefix}{content.ContentUri}", content.Id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
