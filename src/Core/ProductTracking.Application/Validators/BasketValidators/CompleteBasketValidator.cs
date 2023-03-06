using FluentValidation;
using ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket;
using ProductTracking.Application.Features.Commands.BasketCommands.RemoveBasketItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.BasketValidators
{
    internal class CompleteBasketValidator: AbstractValidator<CompleteBasketCommandRequest>
    {
        public CompleteBasketValidator()
        {
            RuleFor(x => x.BasketId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Basket Id boş geçilemez");
        }
    }
}
