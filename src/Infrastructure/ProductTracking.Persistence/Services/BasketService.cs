using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.MongoDb;
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
        private readonly IMongoDbService _mongoDbService;

        public BasketService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IUserService userService, IMongoDbService mongoDbService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mongoDbService = mongoDbService;
        }

        public async Task<Basket> ContextUser(string categoryId)//basket var geri döndürür yoksa yeni oluşturur
        {
            AppUser getUser = await _userService.GetOnlineUserAsync();

            if (getUser != null)
            {
                AppUser user = await _userManager.Users
                         .Include(u => u.Baskets)
                         .FirstOrDefaultAsync(u => u.UserName == getUser.UserName);

                Basket targetBasket = null;
                if (user.Baskets.Any(x => x.CategoryId.ToString() == categoryId && x.IsComplete == false))
                    targetBasket = user.Baskets.FirstOrDefault(x => x.CategoryId.ToString() == categoryId);
                else
                {
                    Category category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                    targetBasket = new() { CategoryId = Guid.Parse(categoryId), Name = $"{category.Name} Sepeti" };
                    user.Baskets.Add(targetBasket);
                    await _unitOfWork.CommitAsync();
                }

                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hatayla karşılaşıldı...");
        }

        public async Task AddItemToBasketAsync(CreateBasketItemDto basketItem)
        {
            Product product = await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId);
            Basket basket = await ContextUser(product.CategoryId.ToString());
            Basket generalBasket = await _unitOfWork.BasketRepository.GetSingleBasketWithPropertiesAsync(x => x.CategoryId == null && x.UserId == basket.UserId && x.IsComplete == false);

            BasketItem _basketItem = await _unitOfWork.BasketItemRepository.GetSingleAsync(x => x.BasketId == basket.Id && x.ProductId == Guid.Parse(basketItem.ProductId));
            if (_basketItem != null)
            {
                _basketItem.Quantity += basketItem.Quantity;
                _basketItem.TotalPrice = _basketItem.Quantity * (await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId)).UnitPrice;
            }
            else
            {
                await _unitOfWork.BasketItemRepository.AddAsync(new()
                {
                    BasketId = generalBasket.Id,
                    ProductId = Guid.Parse(basketItem.ProductId),
                    Quantity = 0,
                    TotalPrice = basketItem.Quantity * (await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId)).UnitPrice
                });
                await _unitOfWork.BasketItemRepository.AddAsync(new()
                {
                    BasketId = basket.Id,
                    ProductId = Guid.Parse(basketItem.ProductId),
                    Quantity = basketItem.Quantity,
                    TotalPrice = basketItem.Quantity * (await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId)).UnitPrice
                });
                await _unitOfWork.CommitAsync();

            }

            BasketItem _generalBasketItem = await _unitOfWork.BasketItemRepository.GetSingleAsync(x => x.BasketId == generalBasket.Id && x.ProductId == Guid.Parse(basketItem.ProductId));
            _generalBasketItem.Quantity += basketItem.Quantity;
            _generalBasketItem.TotalPrice = _generalBasketItem.Quantity * (await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId)).UnitPrice;


            var newbasket = await _unitOfWork.BasketRepository.GetSingleBasketWithPropertiesAsync(x => x.CategoryId == basket.CategoryId && x.UserId == basket.UserId && x.IsComplete == false); ;
            basket.BasketTotalPrice = 0;
            generalBasket.BasketTotalPrice = 0;
            foreach (var item in newbasket.BasketItems)
                basket.BasketTotalPrice += item.TotalPrice;
            foreach (var item in generalBasket.BasketItems)
                generalBasket.BasketTotalPrice += item.TotalPrice;
            await _unitOfWork.CommitAsync();
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




        public async Task<List<Basket>> SearchBasketAsync(string searchBasket)
        {
            searchBasket = searchBasket.ToLower();
            AppUser user = await _userService.GetOnlineUserAsync();

            IQueryable<Basket> baskets = _unitOfWork.BasketRepository.GetWhere(x => x.UserId == user.Id).Include(x => x.Category).Include(x => x.BasketItems).ThenInclude(x => x.Product);

            List<Basket> returnBaskets = new();

            foreach (Basket basket in baskets)
            {
                if (basket.Name.ToLower() == searchBasket)
                    returnBaskets.Add(basket);
                else if (basket.Category?.Name.ToLower() == searchBasket)
                    returnBaskets.Add(basket);
                else if (basket.CreatedDate.Year.ToString() == searchBasket)
                    returnBaskets.Add(basket);
                else if (basket.CompletedDate?.Year.ToString() == searchBasket)
                    returnBaskets.Add(basket);
            }

            if (returnBaskets.Count == 0)
                throw new Exception("Aranan Basket Bulunamadı");

            return returnBaskets;
        }

        public async Task CompleteBasketAsync(Guid basketId)
        {
            Basket basket = await _unitOfWork.BasketRepository.GetSingleBasketWithPropertiesAsync(x => x.Id == basketId);
            if (basket.IsComplete)
                throw new Exception("Basket Zaten Onaylı!");
            basket.IsComplete = true;
            basket.CompletedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            await _mongoDbService.CreatAsync(basket);

            if (basket.CategoryId != null)
            {
                Basket generalBasket = await _unitOfWork.BasketRepository.GetSingleBasketWithPropertiesAsync(x => x.CategoryId == null && x.UserId == basket.UserId);
                await EditGeneralBasketAsync(generalBasket, basket.CategoryId.ToString());
            }
            else
                await EditCategoryBasketAsync(basket);

            await _unitOfWork.CommitAsync();

        }

        private async Task EditGeneralBasketAsync(Basket basket, string categoryId)
        {
            basket.BasketTotalPrice = 0;
            foreach (BasketItem basketItem in basket.BasketItems)
            {
                if (basketItem.Product.CategoryId.ToString() == categoryId)
                    await RemoveBasketItemAsync(basketItem.Id.ToString());
                else
                    basket.BasketTotalPrice += basketItem.TotalPrice;
            }
        }

        private async Task EditCategoryBasketAsync(Basket basket)
        {
            foreach (BasketItem BasketItem in basket.BasketItems)
            {
                Basket tempBasket = await _unitOfWork.BasketRepository.GetSingleBasketWithPropertiesAsync(x => x.CategoryId == BasketItem.Product.CategoryId && x.UserId == basket.UserId && x.IsComplete == false);
                if (tempBasket != null)
                    foreach (BasketItem basketItem1 in tempBasket.BasketItems)
                        await RemoveBasketItemAsync(basketItem1.Id.ToString());
                await _unitOfWork.BasketRepository.RemoveByIdAsync(tempBasket.Id.ToString());
            }
            AppUser user = await _userManager.FindByIdAsync(basket.UserId);
            Basket newBasket = new() { Name = $"{user.Name} Genel Sepeti", UserId = user.Id };
            await _unitOfWork.BasketRepository.AddAsync(newBasket);
            user.Baskets.Add(newBasket);
        }
    }
}
