using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandRequest:IRequest<RegisterUserCommandResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
