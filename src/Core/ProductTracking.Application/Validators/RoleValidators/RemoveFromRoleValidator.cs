using FluentValidation;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveFromRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.RoleValidators
{
    internal class RemoveFromRoleValidator : AbstractValidator<RemoveFromRoleCommandRequest>
    {
        public RemoveFromRoleValidator()
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
