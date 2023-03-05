using AutoMapper;
using MediatR;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket
{
    public class SearchBasketQueryHandler : IRequestHandler<SearchBasketQueryRequest, List<SearchBasketQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public SearchBasketQueryHandler(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        public async Task<List<SearchBasketQueryResponse>> Handle(SearchBasketQueryRequest request, CancellationToken cancellationToken)
        {

            List<Basket> baskets=await _basketService.SearchBasketAsync(request.SearchBasket);

            return _mapper.Map<List<SearchBasketQueryResponse>>(baskets);

        }
    }
}
