using MediatR;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser
{
    public class RefreshTokenLoginUserCommandHandler : IRequestHandler<RefreshTokenLoginUserCommandRequest, RefreshTokenLoginUserCommandResponse>
    {

        private readonly IAuthService _authService;
        public RefreshTokenLoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginUserCommandResponse> Handle(RefreshTokenLoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            return new()
            {
                Token = await _authService.RefreshTokenLoginUserAsync(request.RefreshToken)
            };
        }
    }
}
