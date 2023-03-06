using MediatR;
using ProductTracking.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.RoleCommands.RemoveRole
{
    public class RemoveRoleCommandHandler :IRequestHandler<RemoveRoleCommandRequest,RemoveRoleCommandResponse>
    {
        private readonly IRoleService _roleService;
        public RemoveRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<RemoveRoleCommandResponse> Handle(RemoveRoleCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _roleService.DeleteRoleAsync(request.Id);
            return new()
            {
                Succeeded = result,
            };
        }
    }
}
