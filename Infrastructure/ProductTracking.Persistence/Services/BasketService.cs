using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        private async Task<Basket> ContextUser(string categoryId=null)
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(username))
            {
                AppUser user = await _userManager.Users
                         .Include(u => u.Baskets)
                         .FirstOrDefaultAsync(u => u.UserName == username);

                Basket targetBasket = null;
                if (user.Baskets.Any(x => x.CategoryId.ToString() == categoryId))
                    targetBasket = user.Baskets.FirstOrDefault(x => x.CategoryId.ToString() == categoryId);
                else
                {
                    targetBasket = new();
                    targetBasket.CategoryId = Guid.Parse(categoryId);
                    user.Baskets.Add(targetBasket);
                }

                await _unitOfWork.CommitAsync();

                return targetBasket;          
            }
            throw new Exception("Beklenmeyen bir hatayla karşılaşıldı...");
        }

        public async Task AddItemToBasketAsync(CreateBasketItemDto basketItem)
        {
            Product p = await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId);
            Basket basket = await ContextUser(p.CategoryId.ToString());
            if (basket != null)
            {
                BasketItem _basketItem = await _unitOfWork.BasketItemRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem != null)
                    _basketItem.Quantity++;
                else
                    await _unitOfWork.BasketItemRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    });

                await _unitOfWork.CommitAsync(); 
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket basket = await ContextUser();
            
            Basket result = await _unitOfWork.BasketRepository.GetAll()
                 .Include(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product)
                 .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems
                .ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem basketItem = await _unitOfWork.BasketItemRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _unitOfWork.BasketItemRepository.Remove(basketItem);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemDto basketItem)
        {
            BasketItem _basketItem = await _unitOfWork.BasketItemRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
