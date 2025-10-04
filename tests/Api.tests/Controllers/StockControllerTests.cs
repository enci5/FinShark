using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using api.Controllers;
using api.Interfaces;
using api.Models;
using api.Dtos.Stock;
using api.Mappers;
using api.Helpers;

namespace Api.Tests.Controllers
{

    [TestFixture]
    public class StockControllerTests
    {
        private Mock<IStockRepository> _repoMock;
        private StockController _controller;

        [SetUp]
        public void Setup() 
        {
            _repoMock = new Mock<IStockRepository>();
            _controller = new StockController(_repoMock.Object);
        }

        [Test]
        public async Task GetAll_WhenCalled_ReturnsOkWithStockDtos()
        {
            // Arrange
            var fakeStocks = new List<Stock>
            {
            new Stock { Id = 1, CompanyName = "Apple" },
            new Stock { Id = 2, CompanyName = "Tesla" }
            };

            _repoMock
                .Setup(r => r.GetAllAsync(It.IsAny<QueryObject>()))
                .ReturnsAsync(fakeStocks);

            // Act
            var result = await _controller.GetAll(new QueryObject());

            // ClassicAssert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returned = okResult.Value as List<StockDto>;
            ClassicAssert.AreEqual(2, returned.Count);
            ClassicAssert.AreEqual("Apple", returned[0].CompanyName);
            _repoMock.Verify(r => r.GetAllAsync(It.IsAny<QueryObject>()), Times.Once);
        }

        [Test]
        public async Task GetById_WhenCalled_ReturnsOkWithOneStockDto()
        {
            //Arrange
            var fakeStock = new Stock
            {
                Id = 1,
                CompanyName = "Apple"
            };

            _repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(fakeStock);

            //Act
            var result = await _controller.GetById(1);

            //ClassicAssert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returned = okResult.Value as StockDto;
            ClassicAssert.AreEqual(1, returned.Id);
            ClassicAssert.AreEqual("Apple", returned.CompanyName);
            _repoMock.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetById_WhenCalledWithWrongId_ReturnsNotFound()
        {
            //Arrange
            var fakeStock = new Stock
            {
                Id = 1,
                CompanyName = "Apple"
            };

            _repoMock
                .Setup(r => r.GetByIdAsync(2))
                .ReturnsAsync((Stock)null);

            //Act
            var result = await _controller.GetById(2);

            //ClassicAssert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
            _repoMock.Verify(r => r.GetByIdAsync(2), Times.Once);
        }
    }

}
