using FluentValidation;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.CategoryValidators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("İsim alanı boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("İsim alanı 5-100 karakter arasında olmalıdır");
        }
    }
}
