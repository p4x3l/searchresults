using Microsoft.AspNetCore.Mvc;
using searchresults_api.Contracts;
using System.Threading.Tasks;

namespace searchresults_api.Controllers
{
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("google")]
        public async Task<IActionResult> GetGoogleSearchCount(string searchterms)
        {
            var result = await _searchService.GetNumberOfGoogleHits(searchterms);
            return Ok(result);
        }

        [HttpGet("ebay")]
        public async Task<IActionResult> GetEbaySearchCount(string searchterms)
        {
            var result = await _searchService.GetNumberOfEbayHits(searchterms);
            return Ok(result);
        }

        [HttpGet("yahoo")]
        public async Task<IActionResult> GetYahooSearchCount(string searchterms)
        {
            var result = await _searchService.GetNumberOfYahooHits(searchterms);
            return Ok(result);
        }
    }
}
