using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> RegisterUser(CreateUserDto user);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task<AppUser> GetOnlineUserAsync();
    }
}
