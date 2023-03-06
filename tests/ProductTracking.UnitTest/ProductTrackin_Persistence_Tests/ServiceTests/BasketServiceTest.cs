using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Moq;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Persistence.Services;
using Xunit;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.ServiceTests
{
    public class BasketServiceTest
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMongoDbService> _mongoDbService;
        private readonly BasketService _basketService;
        private readonly DBConfiguration _db;

        public BasketServiceTest()
        {
            _mockUserManager = new Mock<UserManager<AppUser>>(new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Object.UserValidators.Add(new UserValidator<AppUser>());
            _mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserService = new Mock<IUserService>();
            _mongoDbService = new Mock<IMongoDbService>();

            _basketService = new BasketService(_mockUserManager.Object, _mockUnitOfWork.Object, _mockUserService.Object, _mongoDbService.Object);
            _db = new DBConfiguration();
        }

        [Fact]
        public async Task ContextUser_UserIsNotFound_ReturnException()
        {
            AppUser user = null;

            _mockUserService.Setup(x => x.GetOnlineUserAsync())
                .ReturnsAsync(user) ;

            Exception ex = await Assert.ThrowsAsync<Exception>(async ()=>await _basketService.ContextUser(It.IsAny<string>()));

            _mockUserService.Verify(x => x.GetOnlineUserAsync(), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal( "Beklenmeyen bir hatayla karşılaşıldı...", ex.Message);

        }

        [Fact]
        public async Task ContextUser_UserIsFound_ReturnBasket()
        {
           
            _mockUserManager.Setup(x => x.Users)
                .Returns(_db.context.Users);
            _mockUserService.Setup(x => x.GetOnlineUserAsync())
                .ReturnsAsync(await _db.context.Users.FirstOrDefaultAsync());
            _mockUnitOfWork.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(x => x.CategoryRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(new Category());
                
            var result= await _basketService.ContextUser(Guid.NewGuid().ToString());

            _mockUserService.Verify(x => x.GetOnlineUserAsync(), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);

            Assert.IsType<Basket>(result);
        }

        //[Fact]
        //public async Task AddItemToBasketAsync_UserIsFound_ReturnBasket()
        //{
        //    CreateBasketItemDto basketItem = new() { ProductId = context.Products.First().Id.ToString(), Quantity = 50 };

        //    _mockUnitOfWork.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<string>(), false))
        //        .ReturnsAsync(context.Products.First()) ;         
        //    _mockUnitOfWork.Setup(x => x.BasketItemRepository.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<BasketItem, bool>>>(), It.IsAny<bool>()))
        //        .ReturnsAsync(context.BasketItems.First());
        //    _mockUnitOfWork.Setup(x => x.BasketItemRepository.AddAsync(It.IsAny<BasketItem>()))
        //       .ReturnsAsync(true);
        //    _mockUnitOfWork.Setup(x => x.CommitAsync())
        //        .Returns(Task.CompletedTask);



        //    await _basketService.AddItemToBasketAsync(basketItem);

        //    _mockUnitOfWork.Verify(x => x.ProductRepository.GetByIdAsync(It.IsAny<string>(), false), Times.Once);
        //    _mockUnitOfWork.Verify(x => x.BasketItemRepository.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<BasketItem, bool>>>(), It.IsAny<bool>()), Times.Once);
        //    _mockUnitOfWork.Verify(x => x.BasketItemRepository.AddAsync(It.IsAny<BasketItem>()), Times.Once);
        //    _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);

        //}


        [Fact]
        public async Task RemoveBasketItemAsync_BasketItemIsNotFound_ReturnException()
        {
            BasketItem basketItem = null;

            _mockUnitOfWork.Setup(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(basketItem);


            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketService.RemoveBasketItemAsync(It.IsAny<string>()));

            _mockUnitOfWork.Verify(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.BasketItemRepository.Remove(It.IsAny<BasketItem>()), Times.Never);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal("BasketItem Bulunamadı!", ex.Message);
        }


        [Fact]
        public async Task RemoveBasketItemAsync_BasketItemIsFound_RemoveBasketItem()
        {

            _mockUnitOfWork.Setup(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync( await _db.context.BasketItems.FirstOrDefaultAsync()) ;


            await _basketService.RemoveBasketItemAsync(It.IsAny<string>());

            _mockUnitOfWork.Verify(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.BasketItemRepository.Remove(It.IsAny<BasketItem>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);

        }



        [Fact]
        public async Task GetBasketItemsAsync_ActionExecutes_ReturnBasketItems()
        {
            _mockUserService.Setup(x => x.GetOnlineUserAsync())
                .ReturnsAsync(await _db.context.Users.FirstAsync());
            _mockUnitOfWork.Setup(x => x.BasketRepository.GetAll(It.IsAny<bool>()))
                .Returns(_db.context.Baskets);

            var result = await _basketService.GetBasketItemsAsync();

            _mockUserService.Verify(x => x.GetOnlineUserAsync(), Times.Once);
            _mockUnitOfWork.Verify(x => x.BasketRepository.GetAll(It.IsAny<bool>()), Times.Once);

            Assert.IsType<List<BasketItem>>(result);

        }



        [Fact]
        public async Task UpdateQuantityAsync_BasketItemIsNotFound_ReturnException()
        {
            BasketItem basketItem = null;
            UpdateBasketItemDto updateBasketItem = new() { BasketItemId= _db.context.BasketItems.First().ToString(),Quantity=5};
            _mockUnitOfWork.Setup(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(basketItem);


            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketService.UpdateQuantityAsync(updateBasketItem));

            _mockUnitOfWork.Verify(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal("BasketItem Bulunamadı!", ex.Message);
        }

        [Fact]
        public async Task UpdateQuantityAsync_BasketItemIsFound_UpdateBasketItemQuantity()
        {
            BasketItem basketItem = await _db.context.BasketItems.FirstAsync();

            UpdateBasketItemDto updateBasketItem = new() 
            { 
                BasketItemId = basketItem.Id.ToString(), 
                Quantity = 5 
            };
            _mockUnitOfWork.Setup(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(await _db.context.BasketItems.FirstOrDefaultAsync());
            _mockUnitOfWork.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);

            await _basketService.UpdateQuantityAsync(updateBasketItem);

            _mockUnitOfWork.Verify(x => x.BasketItemRepository.GetByIdAsync(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.CommitAsync(), Times.Once);

        }



        [Fact]
        public async Task SearchBasketAsync_BasketIsNotFound_ReturnException()
        {
         
            _mockUserService.Setup(x => x.GetOnlineUserAsync())
                .ReturnsAsync(_db.context.Users.FirstOrDefault());
            _mockUnitOfWork.Setup(x => x.BasketRepository.GetWhere(It.IsAny<System.Linq.Expressions.Expression<System.Func<Basket, bool>>>(), false))
                .Returns(_db.context.Baskets);
                

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketService.SearchBasketAsync("test1"));

            Assert.Equal("Aranan Basket Bulunamadı", ex.Message);
        }

        //[Fact]
        //public async Task SearchBasketAsync_BasketIsFound_ReturnException()
        //{

        //    _mockUserService.Setup(x => x.GetOnlineUserAsync())
        //        .ReturnsAsync(_db.context.Users.FirstOrDefault());
        //    _mockUnitOfWork.Setup(x => x.BasketRepository.GetWhere(It.IsAny<System.Linq.Expressions.Expression<System.Func<Basket, bool>>>(), false))
        //        .Returns(_db.context.Baskets);

        //    string temp = _db.context.Baskets.First().Name;
        //    var result =await _basketService.SearchBasketAsync(temp);

        //    Assert.IsType<List<Basket>>(result);

        //}

    }
}
