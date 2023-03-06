using Castle.Components.DictionaryAdapter.Xml;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;
using ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets;
using ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class BasketsControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly BasketsController _basketsController;
        public BasketsControllerTest()
        {
            _mock = new Mock<IMediator>();
            _basketsController = new BasketsController(_mock.Object);
        }

        [Fact]
        public async Task AddItemToBasket_ActionExecutes_ReturnObjectResultWithAddItemToBasketCommandResponse()
        {

            _mock.Setup(x => x.Send(It.IsAny<AddItemToBasketCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new AddItemToBasketCommandResponse());


            var result = await _basketsController.AddItemToBasket(new AddItemToBasketCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<AddItemToBasketCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var loginUserCommandResponse = Assert.IsAssignableFrom<CustomResponseDto<AddItemToBasketCommandResponse>>(objectResult.Value);

            Assert.Null(loginUserCommandResponse.Errors);
        }

        [Fact]
        public async Task GetBasketItems_ActionExecutes_ReturnObjectResultWithListGetBasketItemsQueryResponse()
        {

            _mock.Setup(x => x.Send(It.IsAny<GetBasketItemsQueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<GetBasketItemsQueryResponse>());


            var result = await _basketsController.GetBasketItems(new GetBasketItemsQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetBasketItemsQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(objectResult.StatusCode, 200);


            var loginUserCommandResponse = Assert.IsAssignableFrom<CustomResponseDto<List<GetBasketItemsQueryResponse>>>(objectResult.Value);

            Assert.Null(loginUserCommandResponse.Errors);
            Assert.NotNull(loginUserCommandResponse.Data);
        }



        [Fact]
        public async Task UpdateBasketItemQuantity_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            UpdateBasketItemQuantityCommandRequest request = new() { BasketItemId = Guid.NewGuid(), Quantity = 10 };


            _mock.Setup(x => x.Send(It.IsAny<UpdateBasketItemQuantityCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UpdateBasketItemQuantityCommandResponse());



            var result = await _basketsController.UpdateBasketItemQuantity(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);

        }


        [Fact]
        public async Task RemoveBasketItem_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            RemoveBasketItemCommandRequest request = new() { BasketItemId = Guid.NewGuid() };


            _mock.Setup(x => x.Send(It.IsAny<RemoveBasketItemCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RemoveBasketItemCommandResponse());



            var result = await _basketsController.RemoveBasketItem(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }


        [Fact]
        public async Task SearchBasket_ActionExecutes_ReturnSearchBasket()
        {
            SearchBasketQueryRequest request = new();


            _mock.Setup(x => x.Send(It.IsAny<RemoveBasketItemCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RemoveBasketItemCommandResponse());



            var result = await _basketsController.SearchBasket(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.NotNull(objectResult.Value);
            Assert.Equal(200, objectResult.StatusCode);
        }
        [Fact]
        public async Task CompleteBasket_ActionExecutes_CompleteBasket()
        {
            CompleteBasketCommandRequest request = new();


            _mock.Setup(x => x.Send(It.IsAny<CompleteBasketCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CompleteBasketCommandResponse());



            var result = await _basketsController.CompleteBasket(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }


        [Fact]
        public async Task GetCompleteBaskets_ActionExecutes_ReturnCompleteList()
        {
            GetCompletedBasketsQueryRequest request = new();


            _mock.Setup(x => x.Send(It.IsAny<GetCompletedBasketsQueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<GetCompletedBasketsQueryResponse>());


            var result = await _basketsController.GetCompleteBaskets(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsType<CustomResponseDto<List<GetCompletedBasketsQueryResponse>>>(objectResult.Value);
            Assert.Null(response.Errors);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
