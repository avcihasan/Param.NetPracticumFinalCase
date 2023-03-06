using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct;
using ProductTracking.Application.Features.Commands.ProductCommands.UpdateProduct;
using ProductTracking.Application.Features.Queries.ProductQueries.GetAllProducts;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;

namespace ProductTracking.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] GetAllProductsQueryRequest getAllProductsQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<GetAllProductsQueryResponse>>.Success(await _mediator.Send(getAllProductsQueryRequest), 200));
        }

        [HttpGet("[action]/{ProductId}")]
        public async Task<IActionResult> GetByIdProduct([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<GetByIdProductQueryResponse>.Success(await _mediator.Send(getByIdProductQueryRequest), 200));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            await _mediator.Send(createProductCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest updateProductCommandRequest)
        {
            await _mediator.Send(updateProductCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> RemoveProduct([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            await _mediator.Send(removeProductCommandRequest);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
