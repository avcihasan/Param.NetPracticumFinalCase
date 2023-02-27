using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;

namespace ProductTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        readonly IBasketService _service;

        public BasketsController(IMediator mediator, IBasketService service)
        {
            _mediator = mediator;
            _service = service;
        }

        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            var x=HttpContext.User.Identity.Name;

            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }



        [Authorize(AuthenticationSchemes = "Admin")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var x = await _service.GetBasketItemsAsync();
            return Ok(x) ;
        }
    }
}
