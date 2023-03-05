using AutoMapper;
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
using ProductTracking.Domain.Entities.MongoDbEntities;

namespace ProductTracking.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMongoDbService _mongoDbService;
        private readonly IMapper _mapper;

        public BasketService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IUserService userService, IMongoDbService mongoDbService, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userService = userService;

            _mongoDbService = mongoDbService;
            _mapper = mapper;
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
                if (user.Baskets.Any(x => x.CategoryId.ToString() == categoryId && x.IsComplete==false))
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
            Product p = await _unitOfWork.ProductRepository.GetByIdAsync(basketItem.ProductId);
            Basket basket = await ContextUser(p.CategoryId.ToString());
            Basket generalBasket = await _unitOfWork.BasketRepository.GetSingleAsync(x => x.CategoryId == null && x.UserId == basket.UserId);
            if (basket != null)
            {
                BasketItem _basketItem = await _unitOfWork.BasketItemRepository.GetSingleAsync(x => x.BasketId == basket.Id && x.ProductId == Guid.Parse(basketItem.ProductId));
                if (_basketItem != null)
                    _basketItem.Quantity+= basketItem.Quantity;
                else
                {
                    await _unitOfWork.BasketItemRepository.AddAsync(new()
                    {
                        BasketId = generalBasket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    });
                      await _unitOfWork.BasketItemRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity
                    });
                }
                  
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

        public async Task<List<Basket>> SearchBasketAsync(string searchBasket)
        {
            searchBasket = searchBasket.ToLower();
            AppUser user =await _userService.GetOnlineUserAsync();

            //IQueryable<Basket> baskets=_unitOfWork.BasketRepository.GetAll().Include(x=>x.Category).Where(x=>x.UserId== user.Id);

            IQueryable<Basket> baskets =_unitOfWork.BasketRepository.GetWhere(x => x.UserId == user.Id).Include(x => x.Category).Include(x=>x.BasketItems).ThenInclude(x=>x.Product);

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
            Basket basket=await _unitOfWork.BasketRepository.GetBasketWithİtems(basketId.ToString());
            basket.IsComplete= true;
            await _unitOfWork.CommitAsync();
            BasketMongoDb basketMongoDb = _mapper.Map<BasketMongoDb>(basket);
            basketMongoDb.CategoryName = basket.Category.Name;

            foreach (BasketItem basketItem in basket.BasketItems)
                basketMongoDb.BasketItems.Add(new() { Quantity=basketItem.Quantity,ProductName=basketItem.Product.Name});
            
            basketMongoDb.Id = null;
            await _mongoDbService.CreatAsync(basketMongoDb);
        }
    }
}
