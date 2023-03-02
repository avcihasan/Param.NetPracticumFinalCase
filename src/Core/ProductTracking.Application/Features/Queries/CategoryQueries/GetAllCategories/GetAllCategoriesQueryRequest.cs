using MediatR;

namespace ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories
{
    public class GetAllCategoriesQueryRequest:IRequest<List<GetAllCategoriesQueryResponse>>
    {
    }
}
