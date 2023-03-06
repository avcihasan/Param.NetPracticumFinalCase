using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.RoleCommands.AddToRole;
using ProductTracking.Application.Features.Commands.RoleCommands.CreateRole;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveFromRole;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveRole;
using ProductTracking.Application.Features.Commands.RoleCommands.UpdateRole;
using ProductTracking.Application.Features.Queries.RoleQueries.GetAllRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class RolesControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly RolesController _rolesController;
        public RolesControllerTest()
        {
            _mock = new Mock<IMediator>();
            _rolesController = new RolesController(_mock.Object);
        }

        [Fact]
        public async Task GetRoles_ActionExecutes_ReturnObjectResultWithRoles()
        {

            _mock.Setup(x => x.Send(It.IsAny<GetAllRolesQueryRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new List<GetAllRolesQueryResponse>());


            var result = await _rolesController.GetRoles(new GetAllRolesQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetAllRolesQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<List<GetAllRolesQueryResponse>>>(objectResult.Value);

            Assert.Null(response.Errors);
        }

        [Fact]
        public async Task CreateRole_ActionExecutes_ReturnObjectResultWithMessage()
        {

            _mock.Setup(x => x.Send(It.IsAny<CreateRoleCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new CreateRoleCommandResponse());


            var result = await _rolesController.CreateRole(new CreateRoleCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<CreateRoleCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<CreateRoleCommandResponse>>(objectResult.Value);

            Assert.Null(response.Errors);
        }


        [Fact]
        public async Task UpdateRole_ActionExecutes_ReturnObjectResultWithMessage()
        {

            _mock.Setup(x => x.Send(It.IsAny<UpdateRoleCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new UpdateRoleCommandResponse());


            var result = await _rolesController.UpdateRole(new UpdateRoleCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<UpdateRoleCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<UpdateRoleCommandResponse>>(objectResult.Value);

            Assert.Null(response.Errors);
        }


        [Fact]
        public async Task DeleteRole_ActionExecutes_ReturnObjectResultWithMessage()
        {

            _mock.Setup(x => x.Send(It.IsAny<RemoveRoleCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new RemoveRoleCommandResponse());


            var result = await _rolesController.DeleteRole(new RemoveRoleCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<RemoveRoleCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<RemoveRoleCommandResponse>>(objectResult.Value);

            Assert.Null(response.Errors);
        }




        [Fact]
        public async Task AddToRole_ActionExecutes_ReturnObjectResultWithMessage()
        {

            _mock.Setup(x => x.Send(It.IsAny<AddToRoleCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new AddToRoleCommandResponse());


            var result = await _rolesController.AddToRole(new AddToRoleCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<AddToRoleCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<AddToRoleCommandResponse>>(objectResult.Value);

            Assert.Null(response.Errors);
        }


        [Fact]
        public async Task RemoveFromRole_ActionExecutes_ReturnObjectResultWithMessage()
        {

            _mock.Setup(x => x.Send(It.IsAny<RemoveFromRoleCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new RemoveFromRoleCommandResponse());


            var result = await _rolesController.RemoveFromRole(new RemoveFromRoleCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<RemoveFromRoleCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var response = Assert.IsAssignableFrom<CustomResponseDto<RemoveFromRoleCommandResponse>>(objectResult.Value);

            Assert.Null(response.Errors);
        }

    }
}
