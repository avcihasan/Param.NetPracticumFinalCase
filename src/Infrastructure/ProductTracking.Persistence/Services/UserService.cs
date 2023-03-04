using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IMapper mapper, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetOnlineUserAsync()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser user = await _userManager.Users
                         .FirstOrDefaultAsync(u => u.UserName == username);
                return user;
            }
            else
                throw new Exception("Kullanıcı bulunamadı!");
        }

        public async Task<CreateUserResponseDto> RegisterUser(CreateUserDto userDto)
        {
            AppUser user = _mapper.Map<AppUser>(userDto);
            user.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = ("Kayıt Başarılı");
            else
                foreach (IdentityError error in result.Errors)
                    response.Message = (error.Description);

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("Kullanıcı bulunamadı");
        }
    }
}
