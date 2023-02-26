using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<TokenDto> LoginUserAsync(LoginUserDto user);
    }
}
