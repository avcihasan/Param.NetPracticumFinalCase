using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.BasketCommandsTests
{
    public class CompleteBasketCommandHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly CompleteBasketCommandHandler _completeBasketCommandHandler;
        public CompleteBasketCommandHandlerTest()
        {
            _mock = new Mock<IBasketService>();
            _completeBasketCommandHandler = new CompleteBasketCommandHandler(_mock.Object);
        }

        [Fact]
        public async Task Handle_ActionExecutes_AddItemToBasketAndReturnAddItemToBasketCommandResponse()
        {
            CompleteBasketCommandRequest request = new() { BasketId=Guid.NewGuid()};
            _mock.Setup(x => x.CompleteBasketAsync(It.IsAny<Guid>()))
                  .Returns(Task.CompletedTask);

            var result = await _completeBasketCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.CompleteBasketAsync(It.IsAny<Guid>()), Times.Once);

            Assert.IsType<CompleteBasketCommandResponse>(result);

        }
    }
}
