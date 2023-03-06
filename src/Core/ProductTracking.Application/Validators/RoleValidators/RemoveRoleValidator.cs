using FluentValidation;
using ProductTracking.Application.Features.Commands.RoleCommands.RemoveRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.RoleValidators
{
    public class RemoveRoleValidator:AbstractValidator<RemoveRoleCommandRequest>
    {
        public RemoveRoleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name boş geçilemez");
        }
    }
}
