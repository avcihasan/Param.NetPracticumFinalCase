using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.RegisterUser;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;

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
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest registerUserCommandRequest)
        {
            RegisterUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
            return CreateActionResult(CustomResponseDto<RegisterUserCommandResponse>.Success(response, 200));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);

            return CreateActionResult(CustomResponseDto<LoginUserCommandResponse>.Success(response, 200));
        }
      
    }
}
