using AutoMapper;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.RegisterUser;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<RegisterUserCommandRequest, AppUser>(); 
            CreateMap<RegisterUserDto, AppUser>(); 
            CreateMap<RegisterUserCommandRequest, RegisterUserDto>();
            CreateMap<RegisterUserResponseDto, RegisterUserCommandResponse>();
            CreateMap<LoginUserCommandRequest, LoginUserDto>();
        }
    }
}
