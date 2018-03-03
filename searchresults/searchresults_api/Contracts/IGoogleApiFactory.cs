using static Google.Apis.Customsearch.v1.CseResource;

namespace searchresults_api.Contracts
{
    public interface IGoogleApiFactory
    {
        ListRequest CreateRequest(string query);
    }
}
