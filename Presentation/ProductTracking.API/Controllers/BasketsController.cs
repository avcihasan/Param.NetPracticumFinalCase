using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;

namespace ProductTracking.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : CustomBaseController
    {
        private readonly IMediator _mediator;
        readonly IBasketService _service;

        public BasketsController(IMediator mediator, IBasketService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<AddItemToBasketCommandResponse>.Success(await _mediator.Send(addItemToBasketCommandRequest), 200));
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest getBasketItemsQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<GetBasketItemsQueryResponse>>.Success(await _mediator.Send(getBasketItemsQueryRequest), 200));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBasketItemQuantity(UpdateBasketItemQuantityCommandRequest updateBasketItemQuantityQueryRequest)
        {
            await _mediator.Send(updateBasketItemQuantityQueryRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
            await _mediator.Send(removeBasketItemCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
