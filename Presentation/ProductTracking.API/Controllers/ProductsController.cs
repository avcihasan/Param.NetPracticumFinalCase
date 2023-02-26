using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;

namespace ProductTracking.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
