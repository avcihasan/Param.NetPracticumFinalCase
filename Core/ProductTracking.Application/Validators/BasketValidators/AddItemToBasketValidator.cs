using FluentValidation;
using ProductTracking.Application.Features.Commands.BasketCommands.AddItemToBasket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.BasketValidators
{
    public class AddItemToBasketValidator:AbstractValidator<AddItemToBasketCommandRequest>
    {
        public AddItemToBasketValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Product Id boş geçilemez");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Quantity boş geçilemez")
                .GreaterThan(0)
                    .WithMessage("Quantity sıfırdan büyük olmalıdır");
                    

        }

        
    }
}
