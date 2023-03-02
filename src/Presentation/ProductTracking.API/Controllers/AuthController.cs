using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser;

namespace ProductTracking.API.Controllers
{
   
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<LoginUserCommandResponse>.Success(await _mediator.Send(loginUserCommandRequest), 200));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginUserCommandRequest refreshTokenLoginUserCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<RefreshTokenLoginUserCommandResponse>.Success(await _mediator.Send(refreshTokenLoginUserCommandRequest), 200));
        }
    }
}
