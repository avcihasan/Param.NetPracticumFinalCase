using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Persistence.Services;
using Xunit;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.ServiceTests
{
    public class AuthServiceTest : DBConfiguration
    {

        //private readonly Mock<UserManager<AppUser>> _mockUserManager;
        //private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
        //private readonly Mock<ITokenHandler> _mockTokenHandler;
        //private readonly Mock<IUserService> _mockUserService;
        //private readonly AuthService _authService;
        //public AuthServiceTest()
        //{
        //    _mockUserManager = new Mock<UserManager<AppUser>>(new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null);
        //    _mockUserManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        //    _mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

        //    _mockSignInManager = new Mock<SignInManager<AppUser>>(
        //              _mockUserManager.Object,
        //             new Mock<IHttpContextAccessor>().Object,
        //             new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
        //             new Mock<IOptions<IdentityOptions>>().Object,
        //             new Mock<ILogger<SignInManager<AppUser>>>().Object,
        //             new Mock<IAuthenticationSchemeProvider>().Object

        //        );

        //    _mockTokenHandler = new Mock<ITokenHandler>();
        //    _mockUserService = new Mock<IUserService>();
        //    _authService = new AuthService(_mockUserManager.Object, _mockSignInManager.Object, _mockTokenHandler.Object, _mockUserService.Object);
        //}


        //[Fact]
        //public async Task LoginUserAsync_ActionExecutesWithExistingUserName_SuccessfulLoginAndReturnTokenDto()
        //{
        //    AppUser user = context.Users.First();
        //    LoginUserDto loginUser = new() { UserNameOrEmail = user.UserName, Password = user.PasswordHash };

        //    _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
        //        .ReturnsAsync(user);
        //    _mockSignInManager.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), false))
        //         .ReturnsAsync(new Microsoft.AspNetCore.Identity.SignInResult());
        //    _mockTokenHandler.Setup(x => x.CreateAccessToken(It.IsAny<int>(), It.IsAny<AppUser>()))
        //        .Returns(new TokenDto());

        //    var result = await _authService.LoginUserAsync(loginUser);

        //    _mockUserManager.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
        //    _mockUserManager.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Never);
        //    _mockSignInManager.Verify(x => x.CheckPasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), false), Times.Never);
        //    _mockTokenHandler.Verify(x => x.CreateAccessToken(It.IsAny<int>(), It.IsAny<AppUser>()), Times.Once);

        //    Assert.IsType<TokenDto>(result);

        //}




    }
}
