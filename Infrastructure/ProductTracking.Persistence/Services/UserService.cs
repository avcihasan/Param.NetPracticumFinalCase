using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.RegisterUser;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserDto userDto)
        {
            AppUser user = _mapper.Map<AppUser>(userDto);
            user.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

            RegisterUserResponseDto response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = ("Kayıt Başarılı");
            foreach (IdentityError error in result.Errors)
                response.Message = (error.Description);
            return response;
        }
    }
}
