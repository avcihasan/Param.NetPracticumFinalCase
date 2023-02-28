using FluentValidation;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.BasketValidators
{
    public class UpdateBasketItemQuantityValidator:AbstractValidator<UpdateBasketItemQuantityCommandRequest>
    {
        public UpdateBasketItemQuantityValidator()
        {
            RuleFor(x => x.BasketItemId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Basket Item Id boş geçilemez");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Quantity boş geçilemez")
                .GreaterThan(0)
                    .WithMessage("Quantity sıfırdan büyük olmalıdır");
        }
    }
}
