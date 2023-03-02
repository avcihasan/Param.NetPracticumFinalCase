using FluentValidation;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.CategoryValidators
{
    public class RemoveCategorValidator:AbstractValidator<RemoveCategoryCommandRequest>
    {
        public RemoveCategorValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Category Id boş geçilemez");
        }
    }
}
