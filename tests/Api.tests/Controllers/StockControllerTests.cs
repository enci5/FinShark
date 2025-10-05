using api.Controllers;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

        [Test]
        public async Task AddNewStock_WhenCalledWithValidBody_ReturnsOkWithOneDto()
        {
            // Arrange
            var fakeStockDto = new CreateStockRequestDto
            {
                CompanyName = "Apple",
                Symbol = "AAPL",
                Purchase = 11111,
                LastDiv = 10,
                Industry = "Tech",
                MarketCap = 88888888
            };

            var fakeStock = new Stock
            {
                Id = 1,
                CompanyName = "Apple",
                Symbol = "AAPL",
                Purchase = 11111,
                LastDiv = 10,
                Industry = "Tech",
                MarketCap = 88888888
            };

            _repoMock
                .Setup(r => r.CreateStockAsync(It.IsAny<Stock>()))
                .ReturnsAsync(fakeStock);

            // Act
            var result = await _controller.AddNewStock(fakeStockDto);

            // Assert
            var createdAtActionObject = result as CreatedAtActionResult;
            ClassicAssert.IsNotNull(createdAtActionObject);
        }

        [Test]
        public async Task AddNewStock_WhenModelStateInvalid_ReturnsBadRequest()
        {
            // Arrange
            var fakeStockDto = new CreateStockRequestDto();
            _controller.ModelState.AddModelError("CompanyName", "Required");

            // Act
            var result = await _controller.AddNewStock(fakeStockDto);

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
            _repoMock.Verify(r => r.CreateStockAsync(It.IsAny<Stock>()), Times.Never);
        }

        [Test]
        public async Task ChangeStock_WhenCalledWithValidData_ReturnsOkWithUpdatedStockDto()
        {
            // Arrange
            var updateDto = new UpdateStockRequestDto
            {
                CompanyName = "Apple Inc.",
                Symbol = "AAPL",
                Purchase = 12000,
                LastDiv = 12,
                Industry = "Technology",
                MarketCap = 90000000
            };

            var updatedStock = new Stock
            {
                Id = 1,
                CompanyName = "Apple Inc.",
                Symbol = "AAPL",
                Purchase = 12000,
                LastDiv = 12,
                Industry = "Technology",
                MarketCap = 90000000
            };

            _repoMock
                .Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateStockRequestDto>()))
                .ReturnsAsync(updatedStock);

            // Act
            var result = await _controller.ChangeStock(1, updateDto);

            // Assert
            var okResult = result as OkObjectResult;
            ClassicAssert.IsNotNull(okResult);
            var returned = okResult.Value as StockDto;
            ClassicAssert.AreEqual(1, returned.Id);
            ClassicAssert.AreEqual("Apple Inc.", returned.CompanyName);
            ClassicAssert.AreEqual(12000, returned.Purchase);
            _repoMock.Verify(r => r.UpdateAsync(1, updateDto), Times.Once);
        }

        [Test]
        public async Task ChangeStock_WhenModelStateInvalid_ReturnsBadRequest()
        {
            // Arrange
            var updateDto = new UpdateStockRequestDto();
            _controller.ModelState.AddModelError("CompanyName", "Required");

            // Act
            var result = await _controller.ChangeStock(1, updateDto);

            // Assert
            ClassicAssert.IsInstanceOf<BadRequestResult>(result);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateStockRequestDto>()), Times.Never);
        }

        [Test]
        public async Task ChangeStock_WhenStockNotFound_ReturnsNotFound()
        {
            // Arrange
            var updateDto = new UpdateStockRequestDto
            {
                CompanyName = "Apple Inc.",
                Symbol = "AAPL"
            };

            _repoMock
                .Setup(r => r.UpdateAsync(It.IsAny<int>(), It.IsAny<UpdateStockRequestDto>()))
                .ReturnsAsync((Stock)null);

            // Act
            var result = await _controller.ChangeStock(999, updateDto);

            // Assert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
            _repoMock.Verify(r => r.UpdateAsync(999, updateDto), Times.Once);
        }

        [Test]
        public async Task Delete_WhenStockExists_ReturnsNoContent()
        {
            // Arrange
            var existingStock = new Stock { Id = 1, CompanyName = "Apple" };

            _repoMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(existingStock);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            ClassicAssert.IsInstanceOf<NoContentResult>(result);
            _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Test]
        public async Task Delete_WhenStockNotFound_ReturnsNotFound()
        {
            // Arrange
            _repoMock
                .Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync((Stock)null);

            // Act
            var result = await _controller.Delete(999);

            // Assert
            ClassicAssert.IsInstanceOf<NotFoundResult>(result);
            _repoMock.Verify(r => r.DeleteAsync(999), Times.Once);
        }

        [Test]
        public async Task GetAll_WithQueryObject_PassesCorrectParametersToRepository()
        {
            // Arrange
            var fakeStocks = new List<Stock> { new Stock { Id = 1, CompanyName = "Test" } };
            var queryObject = new QueryObject
            {
                PageNumber = 2,
                PageSize = 10,
                SortBy = "CompanyName",
                IsAscending = true
            };

            _repoMock
                .Setup(r => r.GetAllAsync(It.IsAny<QueryObject>()))
                .ReturnsAsync(fakeStocks);

            // Act
            var result = await _controller.GetAll(queryObject);

            // Assert
            _repoMock.Verify(r => r.GetAllAsync(It.Is<QueryObject>(q =>
                q.PageNumber == 2 &&
                q.PageSize == 10 &&
                q.SortBy == "CompanyName" &&
                q.IsAscending == true
            )), Times.Once);
        }
    }

}
