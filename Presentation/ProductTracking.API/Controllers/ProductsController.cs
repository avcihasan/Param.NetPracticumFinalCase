using MediatR;
using Microsoft.AspNetCore.Mvc;

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
