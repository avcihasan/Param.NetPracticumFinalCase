using Microsoft.Extensions.Configuration;
using Moq;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Infrastructure.Services.Token;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Infrastructure_Tests.ServicesTest
{
    public class TokenHandlerTest
    {
        private readonly Mock<IConfiguration> _mock;
        private readonly TokenHandler _tokenHandler;
        private  AppUser _user;
        public TokenHandlerTest()
        {
            _mock = new Mock<IConfiguration>();
            _tokenHandler = new TokenHandler(_mock.Object);
            _user = new() { Email = "deneme@deneme.com", Name = "deneme",UserName="deneme" };
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CreateAccessToken_ActionExecutes_ReturnTokenDto(int second)
        {
            _mock.Setup(x => x["Token:SigninKey"]).Returns("SigninKeySigninKey");
            _mock.Setup(x => x["Token:Audience"]).Returns("Audience");
            _mock.Setup(x => x["Token:Issuer"]).Returns("Issuer");

            var result=_tokenHandler.CreateAccessToken(second, _user);

            _mock.Verify(x => x["Token:SigninKey"], Times.Once);
            _mock.Verify(x => x["Token:Audience"], Times.Once);
            _mock.Verify(x => x["Token:Issuer"], Times.Once);


            Assert.IsType<TokenDto>(result);

        }

        [Fact]    
        public void CreateRefreshToken_ActionExecutes_ReturnRefreshToken()
        {
            var result = _tokenHandler.CreateRefreshToken();
            Assert.IsType<string>(result);
        }


    }
}
