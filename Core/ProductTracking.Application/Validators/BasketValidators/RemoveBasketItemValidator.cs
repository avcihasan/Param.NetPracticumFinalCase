using FluentValidation;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.BasketValidators
{
    public class RemoveBasketItemValidator:AbstractValidator<RemoveBasketItemCommandRequest>
    {
        public RemoveBasketItemValidator()
        {
            RuleFor(x => x.BasketItemId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Product Id boş geçilemez");
        }
    }
}
