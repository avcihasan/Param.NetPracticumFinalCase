using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.QueryTests.BasketQueriesTests
{
    public class GetBasketItemsQueryHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly GetBasketItemsQueryHandler _getBasketItemsQueryHandler;
        public GetBasketItemsQueryHandlerTest()
        {
            _mock = new Mock<IBasketService>();
            _getBasketItemsQueryHandler = new GetBasketItemsQueryHandler(_mock.Object);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnListGetBasketItemsQueryResponseWithBasketItems()
        {
            _mock.Setup(x => x.GetBasketItemsAsync())
                .ReturnsAsync(new List<BasketItem>());
            var result = await _getBasketItemsQueryHandler.Handle(It.IsAny<GetBasketItemsQueryRequest>(), It.IsAny<CancellationToken>());

            _mock.Verify(x => x.GetBasketItemsAsync(), Times.Once);

            Assert.IsType<List<GetBasketItemsQueryResponse>>(result);

        }
    }
}
