using Azure.Core;
using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenDto> LoginUserAsync(LoginUserDto userDto)
        {
            AppUser user = await _userManager.FindByNameAsync(userDto.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userDto.UserNameOrEmail);
            if (user == null)
                throw new Exception("Kullanıcı Kaytılı Değil!");
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

            if (result.Succeeded)
            {
                TokenDto token = _tokenHandler.CreateAccessToken(5,user);
                return token;
            }

            throw new Exception("Hatalı Giriş!");
        }
    }
}
