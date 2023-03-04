﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public BasketService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IUserService userService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Basket> ContextUser(string categoryId)
        {
            AppUser getUser = await _userService.GetOnlineUserAsync();
          
            if (getUser != null)
            {
                AppUser user = await _userManager.Users
                         .Include(u => u.Baskets)
                         .FirstOrDefaultAsync(u => u.UserName == getUser.UserName);

                Basket targetBasket = null;
                if (user.Baskets.Any(x => x.CategoryId.ToString() == categoryId))
                    targetBasket = user.Baskets.FirstOrDefault(x => x.CategoryId.ToString() == categoryId);
                else
                {
                    targetBasket = new();
                    targetBasket.CategoryId = Guid.Parse(categoryId);
                    user.Baskets.Add(targetBasket);
                    await _unitOfWork.CommitAsync();
                }

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
            AppUser user = await _userService.GetOnlineUserAsync();
            List<BasketItem> basketItems = new();

            List<Basket> baskets = _unitOfWork.BasketRepository.GetAll()
                 .Include(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product).Where(x => x.UserId == user.Id).ToList();

            foreach (Basket basket in baskets)
                foreach (BasketItem baksetItem in basket.BasketItems)
                    basketItems.Add(baksetItem);
          
            return basketItems;
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem basketItem = await _unitOfWork.BasketItemRepository.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _unitOfWork.BasketItemRepository.Remove(basketItem);
                await _unitOfWork.CommitAsync();
            }
            else
                throw new Exception("BasketItem Bulunamadı!");
        }

        public async Task UpdateQuantityAsync(UpdateBasketItemDto basketItem)
        {
            BasketItem _basketItem = await _unitOfWork.BasketItemRepository.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _unitOfWork.CommitAsync();
            }
            else
                throw new Exception("BasketItem Bulunamadı!");
        }
    }
}
