using Microsoft.Extensions.Options;
using searchresults_api.Configuration;
using searchresults_api.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace searchresults_api.Factory
{
    public class YahooRequestFactory : IYahooRequestFactory
    {
        private readonly string searchUri;

        public YahooRequestFactory(IOptions<YahooConfiguration> settings)
        {
            searchUri = settings.Value.SearchUri;
        }

        public async Task<string> ExecuteRequest(string query)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(string.Format("{0}?p={1}", searchUri, query)))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
