using RestSharp;

namespace searchresults_api.Contracts
{
    public interface IRestClientFactory
    {
        RestClient Create(string baseUrl);

        RestRequest CreateRequest(string url, Method method);
    }
}
