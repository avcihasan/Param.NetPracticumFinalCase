using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.BasketCommandsTests
{
    public class RemoveBasketItemCommandHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly RemoveBasketItemCommandHandler _removeBasketItemCommandHandler;
        public RemoveBasketItemCommandHandlerTest()
        {
            _mock = new Mock<IBasketService>();
            _removeBasketItemCommandHandler = new RemoveBasketItemCommandHandler(_mock.Object);
        }

        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionExecutes_RemoveBasketItemAndReturnRemoveBasketItemCommandResponse(Guid basketItemId)
        {
            RemoveBasketItemCommandRequest request = new() { BasketItemId= basketItemId };
            _mock.Setup(x => x.RemoveBasketItemAsync(It.IsAny<string>()))
                  .Returns(Task.CompletedTask);

            var result = await _removeBasketItemCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.RemoveBasketItemAsync(request.BasketItemId.ToString()), Times.Once);

            Assert.IsType<RemoveBasketItemCommandResponse>(result);

        }
    }
}
