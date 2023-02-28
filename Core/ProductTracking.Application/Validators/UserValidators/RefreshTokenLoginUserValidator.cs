using FluentValidation;
using ProductTracking.Application.Features.Commands.UserCommands.RefreshTokenLoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.UserValidators
{
    public class RefreshTokenLoginUserValidator:AbstractValidator<RefreshTokenLoginUserCommandRequest>
    {
        public RefreshTokenLoginUserValidator()
        {

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Refresh Token Zorunlu alan")
                .Length(5)
                .WithMessage("Refresh Token 32 karakter uzunluğunda olamlıdır");


        }
    }
}
