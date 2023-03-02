using FluentValidation;
using ProductTracking.Application.Features.Commands.UserCommands.LoginUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.UserValidators
{
    public class LoginUserValidator:AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Email/Kullanıcı adı boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(3)
                    .WithMessage("Email/Kullanıcı adı 3-100 karakter arasında olmalıdır");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Şifre boş geçilemez");
                
        }
    }
}
