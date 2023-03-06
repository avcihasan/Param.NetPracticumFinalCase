using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.RoleCommands.RemoveFromRole
{
    public class RemoveFromRoleCommandRequest:IRequest<RemoveFromRoleCommandResponse>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
