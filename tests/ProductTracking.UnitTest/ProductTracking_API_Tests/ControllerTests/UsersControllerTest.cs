using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class UsersControllerTest
    {

        private readonly Mock<IMediator> _mock;
        private readonly UsersController _userController;
        public UsersControllerTest()
        {
            _mock = new Mock<IMediator>();
            _userController = new UsersController(_mock.Object);
        }

       

        [Fact]
        public async Task CreateProduct_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            _mock.Setup(m => m.Send(It.IsAny<CreateUserCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateUserCommandResponse() { Message = "Kayıt Başarılı", Succeeded = true });

            var result = await _userController.CreateUser(It.IsAny<CreateUserCommandRequest>());

            _mock.Verify(x => x.Send(It.IsAny<CreateUserCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, objectResult.StatusCode);

            var createUserCommandResponse = Assert.IsAssignableFrom<CustomResponseDto<CreateUserCommandResponse>>(objectResult.Value);

            Assert.Null(createUserCommandResponse.Errors);
            Assert.True(createUserCommandResponse.Data.Succeeded);


        }


    }
}
