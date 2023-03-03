using Microsoft.EntityFrameworkCore;
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
    public class BasketRepositoryTest:DBConfiguration
    {
        private readonly BasketRepository _basketRepository;
        public BasketRepositoryTest()
        {
            _basketRepository = new BasketRepository(context);
        }

        [Fact]
        public async Task AddAsync_AddingBasket_CreateBasketAndReturnTrue( )
        {
            Basket basket = new() { CategoryId=context.Categories.First().Id,UserId=context.Users.First().Id };
            var result = await _basketRepository.AddAsync(basket);
            await context.SaveChangesAsync();

            var basketResult = context.Baskets.LastOrDefault();

            Assert.IsType<bool>(result);
            Assert.IsType<Basket>(basketResult);

            Assert.Equal(result, true);

            Assert.Equal(basket.CategoryId, basketResult.CategoryId);
        }

        [Fact]
        public async Task AddRangeAsync_AddingBaskets_CreateBasketsAndReturnTrue()
        {
            List<Basket> baskets = new()
            {
                new() { CategoryId=context.Categories.First().Id,UserId=context.Users.First().Id },
                new() { CategoryId=context.Categories.Last().Id,UserId=context.Users.Last().Id }
            };

            var beforeRecording = context.Baskets.Count();

            var result = await _basketRepository.AddRangeAsync(baskets);
            await context.SaveChangesAsync();
            var afterRecording = context.Baskets.Count();

            var lastBasket = context.Categories.LastOrDefault();

            Assert.IsType<bool>(result);
            Assert.Equal(beforeRecording + baskets.Count, afterRecording);

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
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnBasket(bool tracking)
        {
            var basket = context.Baskets.FirstOrDefault();

            var result = await _basketRepository.GetByIdAsync(basket.Id.ToString(), tracking);

            Assert.IsType<Basket>(result);
            Assert.Equal(basket.Id, result.Id);
            Assert.Equal(basket.CategoryId, result.CategoryId);
            Assert.Equal(basket.CreatedDate, result.CreatedDate);
        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveBasketAndReturnTrue()
        {
            var basket = context.Baskets.FirstOrDefault();

            var result = _basketRepository.Remove(basket);
            await context.SaveChangesAsync();

            var newBasket = await context.Categories.Where(x => x.Id == basket.Id).FirstOrDefaultAsync();

            Assert.Null(newBasket);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveBasketAndReturnTrue()
        {
            var basket = context.Baskets.FirstOrDefault();

            var result = await _basketRepository.RemoveByIdAsync(basket.Id.ToString());
            await context.SaveChangesAsync();

            var newBasket = context.Categories.Where(x => x.Id == basket.Id).FirstOrDefault();

            Assert.Equal(result, true);
            Assert.Null(newBasket);
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
