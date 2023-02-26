using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.UserDTOs;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {

        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IMapper mapper, IUserService service)
        {
            _mapper = mapper;
            _service = service;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            RegisterUserResponseDto response = await _service.RegisterUser(_mapper.Map<RegisterUserDto>(request));
            return _mapper.Map<RegisterUserCommandResponse>(response);
        }
    }
}
