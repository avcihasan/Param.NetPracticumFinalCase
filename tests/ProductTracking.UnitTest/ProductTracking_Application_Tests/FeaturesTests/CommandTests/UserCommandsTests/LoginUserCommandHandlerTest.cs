using AutoMapper;
using Moq;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Mapping;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.UserCommandsTests
{
    public class LoginUserCommandHandlerTest
    {
        private readonly Mock<IAuthService> _mock;
        private readonly IMapper _mapper;
        private readonly LoginUserCommandHandler _loginUserCommandHandler;
        public LoginUserCommandHandlerTest()
        {
            _mock = new Mock<IAuthService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _loginUserCommandHandler = new LoginUserCommandHandler(_mock.Object,_mapper);
        }


        [Fact]
        public async Task Handle_ActionExecutes_SuccessfulLoginAndReturnLoginUserCommandResponse()
        {
            _mock.Setup(x => x.LoginUserAsync(It.IsAny<LoginUserDto>()))
            .ReturnsAsync(new TokenDto());

            var result = await _loginUserCommandHandler.Handle(It.IsAny<LoginUserCommandRequest>(), It.IsAny<CancellationToken>());

            _mock.Verify(x => x.LoginUserAsync(It.IsAny<LoginUserDto>()), Times.Once);

            Assert.IsType<LoginUserCommandResponse>(result);
        }
    }
}
