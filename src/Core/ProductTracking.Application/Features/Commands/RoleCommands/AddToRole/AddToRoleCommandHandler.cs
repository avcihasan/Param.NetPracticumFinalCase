using MediatR;
using ProductTracking.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.RoleCommands.AddToRole
{
    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommandRequest, AddToRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public AddToRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<AddToRoleCommandResponse> Handle(AddToRoleCommandRequest request, CancellationToken cancellationToken)
        {
          bool result =await _roleService.AddToRoleAsync(request.UserId,request.RoleName);
            AddToRoleCommandResponse response = new();
            if (result)
                response.Message = "Rol atama başarılı.";
            else
                response.Message = "Rol atama başarısız.";
            return response;
        }
    }
}
