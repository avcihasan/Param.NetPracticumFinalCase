using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.RoleCommands.AddToRole;
using ProductTracking.Application.Features.Commands.RoleCommands.CreateRole;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveFromRole;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveRole;
using ProductTracking.Application.Features.Commands.RoleCommands.UpdateRole;
using ProductTracking.Application.Features.Queries.RoleQueries.GetAllRoles;

namespace ProductTracking.API.Controllers
{

    [Authorize(AuthenticationSchemes = "Admin")]
    [Authorize(Roles = "admin")]
    public class RolesController : CustomBaseController
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] GetAllRolesQueryRequest getAllRolesQueryRequest)
        {
            return CreateActionResult(CustomResponseDto<List<GetAllRolesQueryResponse>>.Success(await _mediator.Send(getAllRolesQueryRequest),200));
        }
 

        [HttpPost()]
        
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<CreateRoleCommandResponse>.Success(await _mediator.Send(createRoleCommandRequest),200));
        }

        [HttpPut("{Id}")]
        
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<UpdateRoleCommandResponse>.Success(await _mediator.Send(updateRoleCommandRequest),200));
        }

        [HttpDelete("{Id}")]
        
        public async Task<IActionResult> DeleteRole([FromRoute] RemoveRoleCommandRequest removeRoleCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<RemoveRoleCommandResponse>.Success(await _mediator.Send(removeRoleCommandRequest),200));

        }


        [HttpPost("[action]")]

        public async Task<IActionResult> AddToRole([FromBody] AddToRoleCommandRequest addToRoleCommandRequest)
        {
            return CreateActionResult(CustomResponseDto<AddToRoleCommandResponse>.Success(await _mediator.Send(addToRoleCommandRequest), 200));

        }
        [HttpPost("[action]")]

        public async Task<IActionResult> RemoveFromRole([FromBody] RemoveFromRoleCommandRequest removeFromRoleCommand)
        {
            return CreateActionResult(CustomResponseDto<RemoveFromRoleCommandResponse>.Success(await _mediator.Send(removeFromRoleCommand), 200));

        }
    }
}
