using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using searchresults_api.Configuration;
using searchresults_api.Contracts;
using searchresults_api.Services;

namespace searchresults_api.Test
{
    [TestClass]
    public class SearchServiceTest
    {
        IOptions<EbayApiConfiguration> ebayOptions = Options.Create(new EbayApiConfiguration { AppId = "AppId", BaseUrl = "http://mock.se" });
        long? nrOfHits = 500;

        [TestMethod]
        public async Task SearchGoogle_SUCCESS()
        {
            // Arrange
            Mock<IRestClientFactory> restClientFactory = new Mock<IRestClientFactory>();
            Mock<IYahooRequestFactory> yahooRequestFactory = new Mock<IYahooRequestFactory>();

            Mock<IGoogleApiFactory> googleApiFactory = new Mock<IGoogleApiFactory>();
            googleApiFactory.Setup(m => m.ExecuteRequest(It.IsAny<string>())).Returns(Task.FromResult(nrOfHits));

            SearchService service = new SearchService(googleApiFactory.Object, restClientFactory.Object, yahooRequestFactory.Object, ebayOptions);

            // Act
            List<string> searchTerms = new List<string> { "google", "test", "search" };
            var result = await service.GetNumberOfGoogleHits(searchTerms);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("google", result.First().SearchTerm);
            Assert.AreEqual(nrOfHits, result.First().SearchCount);
        }

        [TestMethod]
        public async Task SearchEbay_SUCCESS()
        {
            // Arrange
            string content = "{\"findItemsByKeywordsResponse\":[{\"paginationOutput\":[{\"totalEntries\":[" + nrOfHits + "]}]}]}";

            Mock<IGoogleApiFactory> googleApiFactory = new Mock<IGoogleApiFactory>();
            Mock<IYahooRequestFactory> yahooRequestFactory = new Mock<IYahooRequestFactory>();

            Mock<IRestResponse> response = new Mock<IRestResponse>();
            response.Setup(_ => _.Content).Returns(content);
            Mock<RestClient> restClient = new Mock<RestClient>();
            restClient.Setup(m => m.ExecuteTaskAsync(It.IsAny<RestRequest>())).Returns(Task.FromResult(response.Object));
            Mock<IRestClientFactory> restClientFactory = new Mock<IRestClientFactory>();
            restClientFactory.Setup(m => m.Create(It.IsAny<string>())).Returns(restClient.Object);
            restClientFactory.Setup(m => m.CreateRequest(It.IsAny<string>(), It.IsAny<Method>())).Returns(new RestRequest());

            SearchService service = new SearchService(googleApiFactory.Object, restClientFactory.Object, yahooRequestFactory.Object, ebayOptions);

            // Act
            List<string> searchTerms =  new List<string> { "ebay", "test", "search" };
            var result = await service.GetNumberOfEbayHits(searchTerms);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("ebay", result.First().SearchTerm);
            Assert.AreEqual(nrOfHits, result.First().SearchCount);
        }

        [TestMethod]
        public async Task SearchYahoo_SUCCESS()
        {
            // Arrange
            string content = string.Format("<html><body><header>Test</header><content><div>{0} results</div></content></body></html>", nrOfHits);
            HttpResponseMessage response = new HttpResponseMessage()
            {
                Content = new StringContent(content),
                StatusCode = System.Net.HttpStatusCode.OK,
            };

            Mock<IGoogleApiFactory> googleApiFactory = new Mock<IGoogleApiFactory>();
            Mock<IRestClientFactory> restClientFactory = new Mock<IRestClientFactory>();

            Mock<IYahooRequestFactory> yahooRequestFactory = new Mock<IYahooRequestFactory>();
            yahooRequestFactory.Setup(m => m.ExecuteRequest(It.IsAny<string>())).Returns(Task.FromResult(content));

            SearchService service = new SearchService(googleApiFactory.Object, restClientFactory.Object, yahooRequestFactory.Object, ebayOptions);

            // Act
            List<string> searchTerms = new List<string> { "yahoo", "test", "search" };
            var result = await service.GetNumberOfYahooHits(searchTerms);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("yahoo", result.First().SearchTerm);
            Assert.AreEqual(nrOfHits, result.First().SearchCount);
        }
    }
}
