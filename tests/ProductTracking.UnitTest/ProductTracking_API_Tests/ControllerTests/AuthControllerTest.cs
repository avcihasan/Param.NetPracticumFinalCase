using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser;
using ProductTracking.Application.Repositories;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class AuthControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly AuthController _authController;
        public AuthControllerTest()
        {
            _mock = new Mock<IMediator>();
            _authController = new AuthController(_mock.Object);
        }


        [Fact]
        public async Task Login_ActionExecutes_ReturnObjectResultWithLoginUserCommandResponse()
        {

           _mock.Setup(x => x.Send(It.IsAny<LoginUserCommandRequest>(), It.IsAny<CancellationToken>()))
    .ReturnsAsync(new LoginUserCommandResponse() );


            var result = await _authController.Login(new LoginUserCommandRequest());

            _mock.Verify(x=>x.Send(It.IsAny<LoginUserCommandRequest>(), It.IsAny<CancellationToken>()),Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var loginUserCommandResponse = Assert.IsAssignableFrom<CustomResponseDto<LoginUserCommandResponse>>(objectResult.Value);

            Assert.Null(loginUserCommandResponse.Errors);
        }


        [Fact]
        public async Task RefreshTokenLogin_ActionExecutes_ReturnObjectResultWithRefreshTokenLoginUserCommandResponse()
        {

            _mock.Setup(x => x.Send(It.IsAny<RefreshTokenLoginUserCommandRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new RefreshTokenLoginUserCommandResponse());


            var result = await _authController.RefreshTokenLogin(new RefreshTokenLoginUserCommandRequest());

            _mock.Verify(x => x.Send(It.IsAny<RefreshTokenLoginUserCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var refreshTokenLoginUserCommandResponse = Assert.IsAssignableFrom<CustomResponseDto<RefreshTokenLoginUserCommandResponse>>(objectResult.Value);

            Assert.Null(refreshTokenLoginUserCommandResponse.Errors);
        }

    }
}
