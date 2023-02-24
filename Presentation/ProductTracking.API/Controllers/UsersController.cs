using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.RegisterUser;

namespace ProductTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
        {
            return Ok(await _mediator.Send(registerUserCommandRequest));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            return Ok(await _mediator.Send(loginUserCommandRequest));
        }
    }
}
