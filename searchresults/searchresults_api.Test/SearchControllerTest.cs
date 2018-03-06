
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using searchresults_api.Contracts;
using searchresults_api.Controllers;
using searchresults_api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace searchresults_api.Test
{
    [TestClass]
    public class SearchControllerTest
    {
        List<SearchCounter> searchCountObject = new List<SearchCounter>
        {
            new SearchCounter
            {
                SearchTerm = "test1",
                SearchCount = 5000
            }
        };

        [TestMethod]
        public async Task GetGoogleSearchCount_BadRequestIfNoInput()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfGoogleHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetGoogleSearchCount(new List<string>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetGoogleSearchCount_Ok()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfGoogleHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetGoogleSearchCount(new List<string> { "test", "test2" });

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetYahooSearchCount_BadRequestIfNoInput()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfYahooHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetYahooSearchCount(new List<string>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetYahooSearchCount_Ok()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfYahooHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetYahooSearchCount(new List<string> { "test", "test2" });

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetEbaySearchCount_BadRequestIfNoInput()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfEbayHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetEbaySearchCount(new List<string>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetEbaySearchCount_Ok()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfEbayHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetEbaySearchCount(new List<string> { "test", "test2" });

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetAllSearchCounts_BadRequestIfNoInput()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfGoogleHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));
            searchService.Setup(m => m.GetNumberOfYahooHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));
            searchService.Setup(m => m.GetNumberOfEbayHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetAllSearchCounts(new List<string>());

            // Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetAllSearchCounts_Ok()
        {
            // Arrange
            Mock<ISearchService> searchService = new Mock<ISearchService>();
            searchService.Setup(m => m.GetNumberOfEbayHits(It.IsAny<IEnumerable<string>>())).Returns(Task.FromResult(searchCountObject));

            // Act
            var controller = new SearchController(searchService.Object);
            var response = await controller.GetAllSearchCounts(new List<string> { "test", "test2" });

            // Assert
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }
    }
}
