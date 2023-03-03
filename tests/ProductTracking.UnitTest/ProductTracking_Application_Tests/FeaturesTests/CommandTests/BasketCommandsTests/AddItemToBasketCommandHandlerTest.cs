using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.BasketCommandsTests
{
    public class AddItemToBasketCommandHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly AddItemToBasketCommandHandler _addItemToBasketCommandHandler;
        public AddItemToBasketCommandHandlerTest()
        {
            _mock = new Mock<IBasketService>();
            _addItemToBasketCommandHandler = new AddItemToBasketCommandHandler(_mock.Object);
        }

        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1",10)]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2",20)]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3",310)]
        public async Task Handle_ActionExecutes_AddItemToBasketAndReturnAddItemToBasketCommandResponse(Guid productId,int quantity)
        {
            AddItemToBasketCommandRequest request = new() { ProductId = productId, Quantity = quantity };
            _mock.Setup(x => x.AddItemToBasketAsync(It.IsAny<CreateBasketItemDto>()))
                  .Returns(Task.CompletedTask);

            var result = await _addItemToBasketCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.AddItemToBasketAsync(It.IsAny<CreateBasketItemDto>()), Times.Once);

            Assert.IsType<AddItemToBasketCommandResponse>(result);

        }

    }
}
