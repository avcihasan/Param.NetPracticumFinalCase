using FluentValidation;
using ProductTracking.Application.Features.Commands.RoleCommands.AddToRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.RoleValidators
{
    public class AddToRoleValidator:AbstractValidator<AddToRoleCommandRequest>
    {
        public AddToRoleValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("Name 5-50 karakter arasında olmalıdır");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name boş geçilemez");
                
        }
    }
}
