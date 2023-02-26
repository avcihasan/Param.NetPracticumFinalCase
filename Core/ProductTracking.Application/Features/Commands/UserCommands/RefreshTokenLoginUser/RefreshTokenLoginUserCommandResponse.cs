using ProductTracking.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser
{
    public class RefreshTokenLoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
