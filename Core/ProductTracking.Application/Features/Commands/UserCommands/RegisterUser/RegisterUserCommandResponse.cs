using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandResponse
    {
            public bool Succeeded { get; set; }
            public string Message { get; set; }

    }
}
