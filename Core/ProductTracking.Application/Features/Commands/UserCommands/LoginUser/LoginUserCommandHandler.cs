using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Application.Features.Commands.UserCommands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            TokenDto token = await _authService.LoginUserAsync(_mapper.Map<LoginUserDto>(request));
            return new() { Token = token };
        }
    }
}
