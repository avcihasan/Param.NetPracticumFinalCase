using AutoMapper;
using Moq;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
using ProductTracking.Application.Mapping;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.UserCommandsTests
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IUserService> _mock;
        private readonly IMapper _mapper;
        private readonly CreateUserCommandHandler _createUserCommandHandler;
        public CreateUserCommandHandlerTest()
        {
            _mock = new Mock<IUserService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _createUserCommandHandler = new CreateUserCommandHandler(_mapper,_mock.Object);
        }

        [Fact]
        public async Task Handle_ActionExecutes_CreateUserAndReturnCreateUserCommandResponse()
        {
            _mock.Setup(x => x.RegisterUser(It.IsAny<CreateUserDto>()))
            .ReturnsAsync(new CreateUserResponseDto());

            var result = await _createUserCommandHandler.Handle(It.IsAny<CreateUserCommandRequest>(), It.IsAny<CancellationToken>());

            _mock.Verify(x => x.RegisterUser(It.IsAny<CreateUserDto>()), Times.Once);

            Assert.IsType<CreateUserCommandResponse>(result);

        }

    }
}
