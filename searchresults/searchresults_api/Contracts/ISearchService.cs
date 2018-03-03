using System.Collections.Generic;
using System.Threading.Tasks;
using searchresults_api.Models;

namespace searchresults_api.Contracts
{
    public interface ISearchService
    {
        Task<List<SearchCounter>> GetNumberOfGoogleHits(string searchTermsString);

        Task<List<SearchCounter>> GetNumberOfBingHits(string searchterm);

        Task<List<SearchCounter>> GetNumberOfEbayHits(string searchTermsString);
    }
}
