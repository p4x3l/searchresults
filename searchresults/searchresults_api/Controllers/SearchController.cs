using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using searchresults_api.Contracts;
using searchresults_api.Models;

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

        [HttpPost("google")]
        public async Task<IActionResult> GetGoogleSearchCount([FromBody]IEnumerable<string> searchterms)
        {
            if (searchterms == null || searchterms.Count() == 0)
                return BadRequest("No searchterms in body");

            var results = await _searchService.GetNumberOfGoogleHits(searchterms);
            return Ok(new SearchResultResponse { SearchType = "google", SearchResults = results });
        }

        [HttpPost("ebay")]
        public async Task<IActionResult> GetEbaySearchCount([FromBody]IEnumerable<string> searchterms)
        {
            if (searchterms == null || searchterms.Count() == 0)
                return BadRequest("No searchterms in body");

            var results = await _searchService.GetNumberOfEbayHits(searchterms);
            return Ok(new SearchResultResponse { SearchType = "ebay", SearchResults = results });
        }

        [HttpPost("yahoo")]
        public async Task<IActionResult> GetYahooSearchCount([FromBody]IEnumerable<string> searchterms)
        {
            if (searchterms == null || searchterms.Count() == 0)
                return BadRequest("No searchterms in body");

            var results = await _searchService.GetNumberOfYahooHits(searchterms);
            return Ok(new SearchResultResponse { SearchType = "yahoo", SearchResults = results });
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllSearchCounts([FromBody]IEnumerable<string> searchterms)
        {
            if (searchterms == null || searchterms.Count() == 0)
                return BadRequest("No searchterms in body");

            var googleResults = await _searchService.GetNumberOfGoogleHits(searchterms);

            var ebayResults = await _searchService.GetNumberOfEbayHits(searchterms);

            var yahooResults = await _searchService.GetNumberOfYahooHits(searchterms);

            List<SearchResultResponse> response = new List<SearchResultResponse>
            {
                new SearchResultResponse
                {
                    SearchType = "google",
                    SearchResults = googleResults,
                },
                new SearchResultResponse
                {
                    SearchType = "ebay",
                    SearchResults = ebayResults,
                },
                new SearchResultResponse
                {
                    SearchType = "yahoo",
                    SearchResults = yahooResults,
                },
            };

            return Ok(response);
        }
    }
}
