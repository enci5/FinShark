using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Controllers;
using api.Interfaces;
using api.Models;
using api.Dtos.Stock;
using api.Helpers;

namespace api.Tests
{
    [TestFixture]
    public class StockControllerTests
    {
        private Mock<IStockRepository> _mockRepo;
        private StockController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IStockRepository>();
            _controller = new StockController(_mockRepo.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkWithStocks()
        {
            // Arrange
            var mockStocks = new List<Stock>
            {
                new Stock { Id = 1, Name = "Test Stock", Quantity = 10 },
                new Stock { Id = 2, Name = "Another Stock", Quantity = 20 }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<QueryObject>())).ReturnsAsync(mockStocks);

            // Act
            var result = await _controller.GetAll(new QueryObject());

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStocks = okResult.Value as List<StockDto>;
            Assert.AreEqual(2, returnedStocks.Count);
        }

        [Test]
        public async Task GetById_StockExists_ReturnsOk()
        {
            // Arrange
            var stock = new Stock { Id = 1, Name = "Test Stock", Quantity = 10 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(stock);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedStock = okResult.Value as StockDto;
            Assert.AreEqual("Test Stock", returnedStock.Name);
        }
    }
}
