using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.RepositoryTests
{
    public class BasketItemRepositoryTest:DBConfiguration
    {
        private readonly BasketItemRepository _basketItemRepository;
        public BasketItemRepositoryTest()
        {
            _basketItemRepository = new BasketItemRepository(context);
        }

        [Fact]
        public async Task AddAsync_AddingBasketItem_CreateBasketItemAndReturnTrue()
        {
            Product product = await context.Products.FirstAsync();
            Basket basket = await context.Baskets.FirstAsync();

            BasketItem basketItem = new() { ProductId= product.Id,BasketId= basket.Id,Quantity=21 };
            var result = await _basketItemRepository.AddAsync(basketItem);
            await context.SaveChangesAsync();


            Assert.IsType<bool>(result);

            Assert.True(result);

        }

        [Fact]
        public async Task AddRangeAsync_AddingBaskets_CreateBasketItemsAndReturnTrue()
        {
            Product firstProduct = await context.Products.FirstOrDefaultAsync();
            Product lastProduct = await context.Products.LastOrDefaultAsync();

            List<BasketItem> basketItems = new()
            {
                new() { ProductId=firstProduct.Id,Quantity=21 },
                new() { ProductId=lastProduct.Id,Quantity=22 }
            };

            var beforeRecording = await context.Baskets.CountAsync();

            var result = await _basketItemRepository.AddRangeAsync(basketItems);
            await context.SaveChangesAsync();


            Assert.IsType<bool>(result);

            Assert.True(result);

        }


        //[Theory]
        //[InlineData(true)]
        //[InlineData(false)]
        //public void GetAll_ActionExecutes_ReturnCategories(bool tracking)
        //{
        //    var result = _basketRepository.GetAll(tracking);

        //    Assert.IsType<IQueryable<Category>>(result);


        //}

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_InvalidId_ReturnException(bool tracking)
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketItemRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal( "Entity Bulunamadı!", ex.Message);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnBasketItem(bool tracking)
        {
            var basketItem =await context.BasketItems.FirstOrDefaultAsync();

            var result = await _basketItemRepository.GetByIdAsync(basketItem.Id.ToString(), tracking);

            Assert.IsType<BasketItem>(result);
            Assert.Equal(basketItem.Id, result.Id);
            Assert.Equal(basketItem.ProductId, result.ProductId);
         

        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveBasketItemAndReturnTrue()
        {
            var basketItem =await context.BasketItems.FirstOrDefaultAsync();

            var result = _basketItemRepository.Remove(basketItem);
            await context.SaveChangesAsync();

            var newBasket = await context.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefaultAsync();

            Assert.Null(newBasket);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketItemRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal("Entity Bulunamadı!", ex.Message);
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveBasketItemAndReturnTrue()
        {
            var basketItem = await context.BasketItems.LastAsync();

            var result = await _basketItemRepository.RemoveByIdAsync(basketItem.Id.ToString());
            await context.SaveChangesAsync();

            var newBasketItem = await context.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefaultAsync();

            Assert.True(result);
            Assert.Null(newBasketItem);
        }

        //[Fact]
        //public async Task Update_ActionExecutes_UpdateCategoryAndReturnTrue()
        //{
        //    var category = context.Categories.FirstOrDefault().Id;

        //    Category updateCategory = new() { Id = category, Name = "Test Kategori Update" };


        //    var result = _basketRepository.Update(updateCategory);
        //    await context.SaveChangesAsync();

        //    var newCategory = context.Categories.Where(x => x.Id == category).FirstOrDefault();

        //    Assert.Equal(result, true);
        //    Assert.NotNull(newCategory);
        //    Assert.Equal(newCategory.Name, updateCategory.Name);
        //}


        //[Fact]
        //public async Task GetWhere_ActionExecutes_ReturnCategory()
        //{

        //}
        //[Fact]
        //public async Task GetSingleAsync_ActionExecutes_ReturnCategory()
        //{

        //}
    }
}
