using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket
{
    public class SearchBasketQueryRequest:IRequest<List<SearchBasketQueryResponse>>
    {
        public string SearchBasket { get; set; }
    }
}
