using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;

namespace ProductTracking.API.Controllers
{

    public class UsersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest registerUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
            return CreateActionResult(CustomResponseDto<CreateUserCommandResponse>.Success(response, 200));
        }

        
      
    }
}
