using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using searchresults_api.Configuration;
using searchresults_api.Contracts;
using searchresults_api.Models;

namespace searchresults_api.Services
{
    public class SearchService : ISearchService
    {
        private readonly IGoogleApiFactory _googleApiFactory;
        private readonly IRestClientFactory _restClientFactory;
        private readonly IYahooRequestFactory _yahooRequestFactory;
        private readonly IOptions<EbayApiConfiguration> _ebaySettings;

        public SearchService(IGoogleApiFactory googleApiFactory, IRestClientFactory restClientFactory, IYahooRequestFactory yahooRequestFactory, IOptions<EbayApiConfiguration> ebaySettings)
        {
            _googleApiFactory = googleApiFactory;
            _restClientFactory = restClientFactory;
            _yahooRequestFactory = yahooRequestFactory;
            _ebaySettings = ebaySettings;
        }

        public async Task<List<SearchCounter>> GetNumberOfEbayHits(IEnumerable<string> searchterms)
        {
            // Fetch using rest call
            var client = _restClientFactory.Create(_ebaySettings.Value.BaseUrl);

            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchterms)
            {
                var request = _restClientFactory.CreateRequest(
                    string.Format(
                        "?OPERATION-NAME=findItemsByKeywords" +
                        "&SERVICE-VERSION=1.0.0" +
                        "&SECURITY-APPNAME={0}" +
                        "&RESPONSE-DATA-FORMAT=JSON" +
                        "&REST-PAYLOAD" +
                        "&keywords={1}",
                        _ebaySettings.Value.AppId,
                        searchterm),
                    RestSharp.Method.GET);

                var response = await client.ExecuteTaskAsync(request);

                dynamic content = JsonConvert.DeserializeObject(response.Content);
                var totalEntries = content.findItemsByKeywordsResponse[0].paginationOutput[0].totalEntries[0];
                searchCountList.Add(new SearchCounter { SearchTerm = searchterm, SearchCount = totalEntries});
            }
            return searchCountList;
        }

        public async Task<List<SearchCounter>> GetNumberOfYahooHits(IEnumerable<string> searchterms)
        {
            // Fetch using httpclient and regex
            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchterms)
            {
                var result = await _yahooRequestFactory.ExecuteRequest(searchterm);

                var numberOfHits = Regex.Match(result, @"(\d{1,3}(?:,\d{1,3})*) results").Groups[1].Value.Replace(",", "");

                // default set numberOfHits if not found
                if (numberOfHits.Length < 1)
                {
                    numberOfHits = "0";
                }

                searchCountList.Add(new SearchCounter { SearchTerm = searchterm, SearchCount = long.Parse(numberOfHits) });
            }
            return searchCountList;
        }

        public async Task<List<SearchCounter>> GetNumberOfGoogleHits(IEnumerable<string> searchterms)
        {
            // Fetch using nuget package
            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchterms)
            {
                var numberOfHits = await _googleApiFactory.ExecuteRequest(searchterm);
                
                searchCountList.Add(new SearchCounter { SearchTerm = searchterm, SearchCount = numberOfHits.Value });
            }

            return searchCountList;
        }
    }
}
