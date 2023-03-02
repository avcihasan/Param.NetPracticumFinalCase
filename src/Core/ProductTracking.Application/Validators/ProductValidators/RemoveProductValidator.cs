using FluentValidation;
using ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.ProductValidators
{
    public class RemoveProductValidator:AbstractValidator<RemoveProductCommandRequest>
    {
        public RemoveProductValidator()
        {
            RuleFor(x => x.ProductId)
               .NotEmpty()
               .NotNull()
                   .WithMessage("Product Id boş geçilemez");
        }
    }
}
