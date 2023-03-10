using FluentValidation;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.ProductValidators
{
    public class CreateProductValidator:AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name boş geçilemez")
                .MaximumLength(100)
                .MinimumLength(5)
                    .WithMessage("Name 5-100 karakter arasında olmalıdır");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Category Id boş geçilemez");

            RuleFor(x => x.UnitPrice)
                .NotEmpty()
                .NotNull()
                    .WithMessage("UnitPrice boş geçilemez")
                .GreaterThan(0)
                    .WithMessage("Unit Price sıfırdan büyük olmalıdır");
        }
    }
}
