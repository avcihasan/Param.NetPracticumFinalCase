using FluentValidation;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Validators.CategoryValidators
{
    public class GetByIdCategoryValidator:AbstractValidator<GetByIdCategoryQueryRequest>
    {
        public GetByIdCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Categor Id boş geçilemez");
        }
    }
}
