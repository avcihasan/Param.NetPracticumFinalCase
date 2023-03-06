using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;
using ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets;
using ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket;
using ProductTracking.Domain.Entities;

namespace ProductTracking.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchBasket([FromBody] SearchBasketQueryRequest searchBasketQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<SearchBasketQueryResponse>>.Success(await _mediator.Send(searchBasketQueryRequest), 200));
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> CompleteBasket([FromBody] CompleteBasketCommandRequest completeBasketCommandRequest)
        {
            await _mediator.Send(completeBasketCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [Authorize(Roles ="admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompleteBaskets([FromQuery] GetCompletedBasketsQueryRequest getCompletedBasketsQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<GetCompletedBasketsQueryResponse>>.Success(await _mediator.Send(getCompletedBasketsQueryRequest),200));
        }
    }
}
