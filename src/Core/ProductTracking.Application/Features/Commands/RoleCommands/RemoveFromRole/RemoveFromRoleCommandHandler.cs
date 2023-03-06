using MediatR;
using ProductTracking.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.RoleCommands.RemoveFromRole
{
    public class RemoveFromRoleCommandHandler : IRequestHandler<RemoveFromRoleCommandRequest, RemoveFromRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public RemoveFromRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<RemoveFromRoleCommandResponse> Handle(RemoveFromRoleCommandRequest request, CancellationToken cancellationToken)
        {
           bool result= await _roleService.RemoveFromRoleAsync(request.UserId, request.RoleName);
            RemoveFromRoleCommandResponse response = new();
            if (result)
                response.Message = "Rol atama başarılı.";
            else
                response.Message = "Rol atama başarısız.";
            return response;
        }
    }
}
