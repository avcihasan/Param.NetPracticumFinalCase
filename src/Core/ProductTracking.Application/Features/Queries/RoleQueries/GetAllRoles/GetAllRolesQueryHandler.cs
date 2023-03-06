using MediatR;
using ProductTracking.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.RoleQueries.GetAllRoles
{
    internal class GetAllRolesQueryHandler:IRequestHandler<GetAllRolesQueryRequest,List<GetAllRolesQueryResponse>>
    {
        readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            List<GetAllRolesQueryResponse> list = new();
            foreach (var item in _roleService.GetAllRoles())
                list.Add(new() { RoleName=item});

            return list;
        }
    }
}
