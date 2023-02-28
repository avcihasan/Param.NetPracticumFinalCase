using FluentValidation;
using ProductTracking.Application.Features.Commands.UserCommands.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.UserValidators
{
    public class CreateUserValidator:AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Email alanı boş geçilemez")
                .EmailAddress()
                    .WithMessage("Email formatı kullanınız");

            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Username alanı boş geçilemez")
                .MaximumLength(50)
                .MinimumLength(5)
                    .WithMessage("Username alanı 5-50 karakter arasında olmalıdır");

            RuleFor(x=>x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name alanı boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(3)
                    .WithMessage("Name alanı 5-100 karakter arasında olmalıdır");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Surname alanı boş geçilemez")
                .MaximumLength(50)
                .MinimumLength(3)
                    .WithMessage("Surname alanı 3-50 karakter arasında olmalıdır");

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Password alanı boş geçilemez")               
                .MinimumLength(3)
                    .WithMessage("Password alanı en az 3 karakter  olmalıdır");

            RuleFor(x => x.RePassword)
                .NotEmpty()
                .NotNull()
                    .WithMessage("RePassword alanı boş geçilemez")
                .Equal(x => x.Password)
                    .WithMessage("RePassword ile Password Eşleşmiyor");

        }
    }
}
