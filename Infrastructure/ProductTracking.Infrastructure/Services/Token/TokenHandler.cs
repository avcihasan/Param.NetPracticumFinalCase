using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductTracking.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenHandler> _logger;

        public TokenHandler(IConfiguration configuration, ILogger<TokenHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public TokenDto CreateAccessToken(int minute,AppUser user)
        {
            TokenDto token = new();
             
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SigninKey"]));
            
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            
            token.Expiration = DateTime.Now.AddMinutes(minute);

            JwtSecurityToken jwtSecurityToken = new(
                audience: _configuration["Token:SigninKey"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims:new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );

           
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            _logger.LogInformation("############### TOKEN OLUŞTU ###############");
            return token;
        }
    }
}
