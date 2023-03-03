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
            BasketItem basketItem = new() { ProductId=context.Products.First().Id,BasketId=context.Baskets.First().Id,Quantity=21 };
            var result = await _basketItemRepository.AddAsync(basketItem);
            await context.SaveChangesAsync();

            var basketItemResult = context.BasketItems.LastOrDefault();

            Assert.IsType<bool>(result);
            Assert.IsType<BasketItem>(basketItemResult);

            Assert.Equal(result, true);

            Assert.Equal(basketItem.Id, basketItemResult.Id);
        }

        [Fact]
        public async Task AddRangeAsync_AddingBaskets_CreateBasketItemsAndReturnTrue()
        {
            List<BasketItem> basketItems = new()
            {
                new() { ProductId=context.Products.First().Id,Quantity=21 },
                new() { ProductId=context.Products.Last().Id,Quantity=22 }
            };

            var beforeRecording = context.Baskets.Count();

            var result = await _basketItemRepository.AddRangeAsync(basketItems);
            await context.SaveChangesAsync();
            var afterRecording = context.BasketItems.Count();

            var lastBasketItem = context.BasketItems.LastOrDefault();

            Assert.Equal(lastBasketItem.Id, basketItems.LastOrDefault().Id);
            Assert.IsType<bool>(result);
            Assert.Equal(beforeRecording + basketItems.Count, afterRecording);

            Assert.Equal(result, true);

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
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnBasketItem(bool tracking)
        {
            var basketItem = context.BasketItems.FirstOrDefault();

            var result = await _basketItemRepository.GetByIdAsync(basketItem.Id.ToString(), tracking);

            Assert.IsType<BasketItem>(result);
            Assert.Equal(basketItem.Id, result.Id);
            Assert.Equal(basketItem.ProductId, result.ProductId);
         

        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveBasketItemAndReturnTrue()
        {
            var basketItem = context.BasketItems.FirstOrDefault();

            var result = _basketItemRepository.Remove(basketItem);
            await context.SaveChangesAsync();

            var newBasket = await context.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefaultAsync();

            Assert.Null(newBasket);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketItemRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveBasketItemAndReturnTrue()
        {
            var basketItem = context.BasketItems.FirstOrDefault();

            var result = await _basketItemRepository.RemoveByIdAsync(basketItem.Id.ToString());
            await context.SaveChangesAsync();

            var newBasketItem = context.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefault();

            Assert.Equal(result, true);
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
