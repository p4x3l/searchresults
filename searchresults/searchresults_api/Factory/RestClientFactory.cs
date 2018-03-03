using RestSharp;
using searchresults_api.Contracts;

namespace searchresults_api.Factory
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClient Create(string baseUrl)
        {
            return new RestClient(baseUrl );
        }

        public RestRequest CreateRequest(string url, Method method)
        {
            var request = new RestRequest(url, method);
            return request;
        }
    }
}
