using ProductTracking.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.LoginUser
{
    public class LoginUserCommandResponse
    {
        public TokenDto Token { get; set; }
    }
}
