using Moq;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.UserCommandsTests
{
    public class RefreshTokenLoginUserCommandHandlerTest
    {
        private readonly Mock<IAuthService> _mock;
        private readonly RefreshTokenLoginUserCommandHandler _refreshTokenLoginUserCommandHandler;
        public RefreshTokenLoginUserCommandHandlerTest()
        {
            _mock = new Mock<IAuthService>();
            _refreshTokenLoginUserCommandHandler = new RefreshTokenLoginUserCommandHandler(_mock.Object);
        }


        [Theory]
        [InlineData("refreshToken-0")]
        [InlineData("refreshToken-1")]
        [InlineData("refreshToken-2")]
        [InlineData("refreshToken-3")]
        [InlineData("refreshToken-4")]
        public async Task Handle_ActionExecutes_ReturnRefreshTokenLoginUserCommandResponse(string refreshToken)
        {
            RefreshTokenLoginUserCommandRequest request = new() { RefreshToken = refreshToken };

            _mock.Setup(x => x.RefreshTokenLoginUserAsync(It.IsAny <string>()))
            .ReturnsAsync(new TokenDto());

            var result = await _refreshTokenLoginUserCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.RefreshTokenLoginUserAsync(It.IsAny<string>()), Times.Once);

            Assert.IsType<RefreshTokenLoginUserCommandResponse>(result);

        }
    }
}
