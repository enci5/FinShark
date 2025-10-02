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
            new Stock { Id = 1, Name = "Apple" },
            new Stock { Id = 2, Name = "Tesla" }
        };

        // Setup repo to return the fake stocks, no matter the QueryObject
        _repoMock
            .Setup(r => r.GetAllAsync(It.IsAny<QueryObject>()))
            .ReturnsAsync(fakeStocks);

        // Act
        var result = await _controller.GetAll(new QueryObject());

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returned = okResult.Value as List<StockDto>;
        Assert.AreEqual(2, returned.Count);
        Assert.AreEqual("Apple", returned[0].Name);

        // Also verify interaction if you want
        _repoMock.Verify(r => r.GetAllAsync(It.IsAny<QueryObject>()), Times.Once);
    }
}
