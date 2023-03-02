using ProductTracking.Application.DTOs.TokenDTOs;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(int seconds, AppUser user);
        string CreateRefreshToken();
    }
}
