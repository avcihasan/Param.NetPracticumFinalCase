using AutoMapper;
using MediatR;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems
{
    public class GetBasketItemsQueryHandler : IRequestHandler<GetBasketItemsQueryRequest, List<GetBasketItemsQueryResponse>>
    {
        private readonly IBasketService _basketService;
        public GetBasketItemsQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<List<GetBasketItemsQueryResponse>> Handle(GetBasketItemsQueryRequest request, CancellationToken cancellationToken)
        {
            List<BasketItem> basketItems = await _basketService.GetBasketItemsAsync();

            return basketItems.Select(x => new GetBasketItemsQueryResponse()
            {
                BasketItemId = x.Id,
                Name = x.Product.Name,
                Price = x.Product.UnitPrice,
                Quantity = x.Quantity
            }).ToList();
        }
    }
}
