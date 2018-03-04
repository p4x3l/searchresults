using System.Collections.Generic;
using System.Threading.Tasks;
using searchresults_api.Models;

namespace searchresults_api.Contracts
{
    public interface ISearchService
    {
        Task<List<SearchCounter>> GetNumberOfGoogleHits(IEnumerable<string> searchterms);

        Task<List<SearchCounter>> GetNumberOfYahooHits(IEnumerable<string> searchterms);

        Task<List<SearchCounter>> GetNumberOfEbayHits(IEnumerable<string> searchterms);
    }
}
