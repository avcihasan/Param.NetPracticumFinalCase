using AutoMapper;
using MediatR;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.UserDTOs;

namespace ProductTracking.Application.Features.Commands.UserCommands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {

        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMapper mapper, IUserService service)
        {
            _mapper = mapper;
            _service = service;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponseDto response = await _service.RegisterUser(_mapper.Map<CreateUserDto>(request));
            return _mapper.Map<CreateUserCommandResponse>(response);
        }
    }
}
