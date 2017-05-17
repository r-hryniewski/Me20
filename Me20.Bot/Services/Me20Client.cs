using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Me20.Bot.Services
{
    //TODO: Refactor!
    public class Me20Client
    {
        private readonly Uri me20BaseUrl;
        public Me20Client()
        {
            me20BaseUrl = new Uri(ConfigurationManager.AppSettings["Me20InstanceUrl"]);
        }
        //TODO: DRY
        public async Task<Dictionary<string, string>> GetByTagAsync(string tag, int count = 5)
        {
            using (var client = new HttpClient() { BaseAddress = me20BaseUrl })
            {
                using (var response = await client.GetAsync($"api/external/content/tagged?tags={tag}"))
                {
                    try
                    {

                        var responseString = await response.Content.ReadAsStringAsync();

                        if (JObject.Parse(responseString).TryGetValue("item", out JToken items))
                        {
                            return ((JArray)items).Select(i =>
                            {
                                return new
                                {
                                    url = i.SelectToken("url")?.ToString(),
                                    title = i.SelectToken("title")?.ToString()
                                };
                            })
                            .GroupBy(x => x.title)
                            .ToDictionary(x => x.Key, x => x.FirstOrDefault()?.url);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return new Dictionary<string, string>();
                }
            }
        }
        //TODO: DRY
        public async Task<string[]> GetTagsListAsync()
        {
            using (var client = new HttpClient() { BaseAddress = me20BaseUrl })
            {
                using (var response = await client.GetAsync($"api/external/tags"))
                {
                    try
                    {

                        var responseString = await response.Content.ReadAsStringAsync();

                        if (JObject.Parse(responseString).TryGetValue("item", out JToken items))
                        {
                            return ((JArray)items).Select(i => i.SelectToken("tagName").ToString())
                            .ToArray();
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return new string[0];
                }
            }
        }
    }
}