using FluentValidation;
using ProductTracking.Application.Features.Commands.RoleCommands.CreateRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.RoleValidators
{
    public class CreateRoleValidator:AbstractValidator<CreateRoleCommandRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("Name 5-50 karakter arasında olmalıdır");
        }
    }
}
