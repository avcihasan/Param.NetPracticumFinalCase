using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = _mapper.Map<AppUser>(request);
            user.Id = Guid.NewGuid().ToString();
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            RegisterUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = ("Kayıt Başarılı");
            foreach (IdentityError error in result.Errors)
                response.Message = (error.Description);
            return response;
        }
    }
}
