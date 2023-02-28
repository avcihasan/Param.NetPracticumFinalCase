using FluentValidation;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.ProductValidators
{
    public class GetByIdProductValidator:AbstractValidator<GetByIdProductQueryRequest>
    {
        public GetByIdProductValidator()
        {
            RuleFor(x => x.ProductId)
               .NotEmpty()
               .NotNull()
                   .WithMessage("Product Id boş geçilemez");
        }
    }
}
