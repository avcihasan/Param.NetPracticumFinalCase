using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.Basket
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(CreateBasketItemDto basketItem);
        public Task UpdateQuantityAsync(UpdateBasketItemDto basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
    }
}
