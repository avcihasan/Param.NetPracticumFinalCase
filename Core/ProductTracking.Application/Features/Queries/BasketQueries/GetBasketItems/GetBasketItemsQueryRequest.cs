using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems
{
    public class GetBasketItemsQueryRequest :IRequest<List<GetBasketItemsQueryResponse>>
    {
    }
}
