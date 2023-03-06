using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.RoleQueries.GetAllRoles
{
    public class GetAllRolesQueryRequest:IRequest<List<GetAllRolesQueryResponse>>
    {
    }
}
