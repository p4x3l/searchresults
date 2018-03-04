using System.Threading.Tasks;

namespace searchresults_api.Contracts
{
    public interface IYahooRequestFactory
    {
        Task<string> ExecuteRequest(string query);
    }
}
