using System.Threading.Tasks;

namespace searchresults_api.Contracts
{
    public interface IGoogleApiFactory
    {
        Task<long?> ExecuteRequest(string query);
    }
}
