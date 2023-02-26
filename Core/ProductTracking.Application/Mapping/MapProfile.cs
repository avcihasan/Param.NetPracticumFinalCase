using AutoMapper;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
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
            CreateMap<CreateUserCommandRequest, AppUser>(); 
            CreateMap<CreateUserDto, AppUser>(); 
            CreateMap<CreateUserCommandRequest, CreateUserDto>();
            CreateMap<CreateUserResponseDto, CreateUserCommandResponse>();
            CreateMap<LoginUserCommandRequest, LoginUserDto>();
        }
    }
}
