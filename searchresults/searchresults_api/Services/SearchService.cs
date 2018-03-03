using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using searchresults_api.Contracts;
using searchresults_api.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace searchresults_api.Services
{
    public class SearchService : ISearchService
    {
        private readonly IGoogleApiFactory _googleApiFactory;
        private readonly IRestClientFactory _restClientFactory;
        private readonly IConfiguration _configuration;

        public SearchService(IConfiguration configuration, IGoogleApiFactory googleApiFactory, IRestClientFactory restClientFactory)
        {
            _googleApiFactory = googleApiFactory;
            _restClientFactory = restClientFactory;
            _configuration = configuration;
        }

        public async Task<List<SearchCounter>> GetNumberOfEbayHits(string searchTermsString)
        {
            // Fetch using rest call
            var client = _restClientFactory.Create(_configuration.GetSection("EbayApi:BaseUrl").Value);

            var searchTermsList = searchTermsString.Split(" ");

            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchTermsList)
            {
                var request = _restClientFactory.CreateRequest(
                    string.Format(
                        "?OPERATION-NAME=findItemsByKeywords" +
                        "&SERVICE-VERSION=1.0.0" +
                        "&SECURITY-APPNAME={0}" +
                        "&RESPONSE-DATA-FORMAT=JSON" +
                        "&REST-PAYLOAD" +
                        "&keywords={1}",
                        _configuration.GetSection("EbayApi:AppId").Value,
                        searchterm),
                    RestSharp.Method.GET);

                var response = await client.ExecuteTaskAsync(request);

                dynamic content = JsonConvert.DeserializeObject(response.Content);
                var totalEntries = content.findItemsByKeywordsResponse[0].paginationOutput[0].totalEntries[0];
                searchCountList.Add(new SearchCounter { SearchTerm = searchterm, SearchCount = totalEntries});
            }
            return searchCountList;
        }

        public async Task<List<SearchCounter>> GetNumberOfBingHits(string searchTermsString)
        {
            // Fetch using httpclient and regex
            var searchTermsList = searchTermsString.Split(" ");

            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchTermsList)
            {
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(string.Format("{0}?p={1}", _configuration.GetSection("Yahoo:SearchUri").Value, searchterm)))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();

                    Regex.Match
                }
            }
            throw new System.NotImplementedException();
        }

        public async Task<List<SearchCounter>> GetNumberOfGoogleHits(string searchTermsString)
        {
            // Fetch using nuget package
            var searchTermsList = searchTermsString.Split(" ");

            var searchCountList = new List<SearchCounter>();

            foreach (var searchterm in searchTermsList)
            {
                var request = _googleApiFactory.CreateRequest(searchterm);
                var results = await request.ExecuteAsync();
                searchCountList.Add(new SearchCounter { SearchTerm = searchterm, SearchCount = results.SearchInformation.TotalResults });
            }

            return searchCountList;
        }
    }
}
