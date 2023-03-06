using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.RoleCommands.RemoveRole
{
    public class RemoveRoleCommandRequest:IRequest<RemoveRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}
