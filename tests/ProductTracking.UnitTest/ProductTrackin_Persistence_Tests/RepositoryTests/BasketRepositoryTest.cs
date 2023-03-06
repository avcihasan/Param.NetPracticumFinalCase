using DnsClient;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.RepositoryTests
{
    public class BasketRepositoryTest 
    {
        private readonly BasketRepository _basketRepository;
        private readonly DBConfiguration _db;
        public BasketRepositoryTest()
        {
            _db=new DBConfiguration();
            _basketRepository = new BasketRepository(_db.context);
        }

        [Fact]
        public async Task AddAsync_AddingBasket_CreateBasketAndReturnTrue()
        {
            Basket basket = new() { Name="Test Basket" };
            var result = await _basketRepository.AddAsync(basket);
            await _db.context.SaveChangesAsync();

            Basket _basket =await _db.context.Baskets.FirstOrDefaultAsync(x=>x.Id==basket.Id);

            Assert.Equal(_basket.Name,basket.Name);

            Assert.IsType<bool>(result);

            Assert.True(result);

        }

        [Fact]
        public async Task AddRangeAsync_AddingBaskets_CreateBasketsAndReturnTrue()
        {
            Basket basket1 = new() { Name = "Test Kategori 1", IsComplete = true };
            Basket basket2 = new() { Name = "Test Kategori 2", IsComplete = false };
       


            var result = await _basketRepository.AddRangeAsync(new() { basket1,basket2});
            await _db.context.SaveChangesAsync();

            Basket _basket1 = await _db.context.Baskets.FirstOrDefaultAsync(x => x.Id == basket1.Id);
            Basket _basket2 = await _db.context.Baskets.FirstOrDefaultAsync(x => x.Id == basket2.Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.NotNull(_basket1);
            Assert.NotNull(_basket2);
            Assert.True(_basket1.IsComplete);
            Assert.False(_basket2.IsComplete);
        }


        //[Theory]
        //[InlineData(true)]
        //[InlineData(false)]
        //public void GetAll_ActionExecutes_ReturnCategories(bool tracking)
        //{
        //    var result = _basketRepository.GetAll(tracking);

        //    Assert.IsAssignableFrom<IQueryable<Category>>(result);


        //}

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_InvalidId_ReturnException(bool tracking)
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal("Entity Bulunamadı!", ex.Message);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnBasket(bool tracking)
        {
            Basket basket =await _db.context.Baskets.FirstOrDefaultAsync();

            var result = await _basketRepository.GetByIdAsync(basket.Id.ToString(), tracking);

            Assert.IsType<Basket>(result);
            Assert.NotNull(result);
            Assert.Equal(basket.Id, result.Id);
            Assert.Equal(basket.Name, result.Name);
        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveBasketAndReturnTrue()
        {
            Basket basket = await _db.context.Baskets.FirstOrDefaultAsync();

            var result = _basketRepository.Remove(basket);
            await _db.context.SaveChangesAsync();

            var _basket = await _db.context.Baskets.Where(x => x.Id == basket.Id).FirstOrDefaultAsync();

            Assert.Null(_basket);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal("Entity Bulunamadı!", ex.Message);
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveBasketAndReturnTrue()
        {
            Basket basket = await _db.context.Baskets.FirstOrDefaultAsync();

            var result = await _basketRepository.RemoveByIdAsync(basket.Id.ToString());
            await _db.context.SaveChangesAsync();

            var _basket = await _db.context.Baskets.Where(x => x.Id == basket.Id).FirstOrDefaultAsync();

            Assert.True(result);
            Assert.Null(_basket);
        }



        [Fact]
        public async Task GetSingleBasketWithPropertiesAsync_InvalidUser_ReturnException()
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketRepository.GetSingleBasketWithPropertiesAsync(x=>x.Name.ToString()=="",false));
            Assert.Equal("Basket Bulunamadı!", ex.Message);
        }
        [Fact]
        public async Task GetSingleBasketWithPropertiesAsync_ValidUser_ReturnException()
        {
            Basket basket = await _db.context.Baskets.FirstOrDefaultAsync();

            var result =await _basketRepository.GetSingleBasketWithPropertiesAsync(x=>x.Id==basket.Id);
            await _db.context.SaveChangesAsync();


            Assert.NotNull(result);
            Assert.Equal(basket.Name, result.Name);
            Assert.Equal(basket.IsComplete, result.IsComplete);
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
