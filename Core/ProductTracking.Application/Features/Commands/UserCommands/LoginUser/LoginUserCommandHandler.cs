using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Application.Features.Commands.UserCommands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user =await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
                throw new Exception("Kullanıcı Kaytılı Değil!");
            SignInResult result=await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);

            if (result.Succeeded)
            {
                TokenDto token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserCommandResponse() { Token = token };
            }

            throw new Exception("Hatalı Giriş!");
        }
    }
}
