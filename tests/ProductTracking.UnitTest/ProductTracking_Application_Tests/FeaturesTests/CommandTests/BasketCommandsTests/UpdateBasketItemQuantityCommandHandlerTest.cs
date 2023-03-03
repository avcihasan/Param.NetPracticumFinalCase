using AutoMapper;
using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Mapping;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.BasketCommandsTests
{
    public class UpdateBasketItemQuantityCommandHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly UpdateBasketItemQuantityCommandHandler _updateBasketItemQuantityCommandHandler;
        private readonly IMapper _mapper;
        public UpdateBasketItemQuantityCommandHandlerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();


            _mock = new Mock<IBasketService>();
            _updateBasketItemQuantityCommandHandler = new UpdateBasketItemQuantityCommandHandler(_mock.Object,_mapper);
        }

        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1", 10)]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2", 20)]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3", 310)]
        public async Task Handle_ActionExecutes_UpdateBasketItemQuantityAndReturnUpdateBasketItemQuantityCommandResponse(Guid productId, int quantity)
        {
            UpdateBasketItemQuantityCommandRequest request = new() { BasketItemId = productId, Quantity = quantity };
            _mock.Setup(x => x.UpdateQuantityAsync(It.IsAny<UpdateBasketItemDto>()))
            .Returns(Task.CompletedTask);

            var result = await _updateBasketItemQuantityCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.UpdateQuantityAsync(It.IsAny<UpdateBasketItemDto>()), Times.Once);

            Assert.IsType<UpdateBasketItemQuantityCommandResponse>(result);

        }
    }
}
