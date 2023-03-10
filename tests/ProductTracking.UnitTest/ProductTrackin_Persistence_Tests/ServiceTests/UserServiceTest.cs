using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.ServiceTests
{
    public class UserServiceTest 
    {
        private readonly IMapper _mapper;
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly UserService _userService;
        private readonly DBConfiguration _db;

        public UserServiceTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUserManager = new Mock<UserManager<AppUser>>(new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Object.UserValidators.Add(new UserValidator<AppUser>());
            _mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _userService = new UserService(_mapper, _mockUserManager.Object, _mockHttpContextAccessor.Object, _mockUnitOfWork.Object);
            _db = new DBConfiguration();
        }


        [Fact]
        public async Task GetOnlineUserAsync_InvalidUserName_ReturnException()
        {
            string userName = null;
            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.Identity.Name)
                .Returns(userName);

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _userService.GetOnlineUserAsync());
            Assert.Equal("Kullanıcı bulunamadı!", ex.Message);

        }

        [Fact]
        public async Task GetOnlineUserAsync_ValidUserName_ReturnAppUser()
        {
            AppUser user = await _db.context.Users.FirstAsync();

            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.Identity.Name)
                .Returns(user.UserName);

            _mockUserManager.Setup(x => x.Users)
               .Returns(_db.context.Users);

            var result = await _userService.GetOnlineUserAsync();

            Assert.IsType<AppUser>(result);
            Assert.Equal(result.UserName, user.UserName);
            Assert.Equal(result.Email, user.Email);

        }



        //[Fact]
        //public async Task RegisterUser_SuccessfulUserRegistration_ReturnCreateUserResponseDto()
        //{
        //    CreateUserDto userDto = new() { Email="testdenem@gmail.com",Name="Test",Surname="Test",Username="test",Password="123",RePassword="123"};

        //    _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(),It.IsAny<string>()))
        //       .ReturnsAsync(IdentityResult.Success);
        //    _mockUnitOfWork.Setup(x=>x.BasketRepository.AddAsync(It.IsAny<Basket>()))
        //         .ReturnsAsync(true);
        //    _mockUnitOfWork.Setup(x => x.CommitAsync())
        //        .Returns(Task.CompletedTask);

        //    var result = await _userService.RegisterUser(userDto);

        //    Assert.IsType<CreateUserResponseDto>(result);
        //    Assert.Equal("Kayıt Başarılı", result.Message);
        //    Assert.True(result.Succeeded);
                
        //}


        [Fact]
        public async Task RegisterUser_UnsuccessfulUserRegistration_ReturnCreateUserResponseDtoWithErrors()
        {
            CreateUserDto userDto = new() { Email = "testdenem@gmail.com", Name = "Test", Surname = "Test", Username = "test", Password = "123", RePassword = "123" };

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
               .ReturnsAsync(IdentityResult.Failed(new IdentityError[] { new IdentityError() { Code="500",Description="Unit Test Kullanıcı Kayıt Hatası!"} }));

            var result = await _userService.RegisterUser(userDto);

            Assert.IsType<CreateUserResponseDto>(result);
            Assert.Equal( "Unit Test Kullanıcı Kayıt Hatası!", result.Message);
            Assert.False(result.Succeeded);

        }


        [Fact]
        public async Task UpdateRefreshToken_UserIsNull_ReturnException()
        {
            AppUser user = null;

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _userService.UpdateRefreshToken("refreshToken", user, DateTime.Now, 1));

            Assert.Equal( "Kullanıcı bulunamadı",ex.Message);

        }

        [Fact]
        public async Task UpdateRefreshToken_UserIsNotNull_UpdateRefreshToken()
        {
            AppUser user =await _db.context.Users.FirstOrDefaultAsync();
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(new IdentityResult());

            await _userService.UpdateRefreshToken("refreshToken", user, DateTime.Now, 1);

            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<AppUser>()));
        }
    }
}
