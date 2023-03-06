using Microsoft.EntityFrameworkCore;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Repositories;
using Xunit;
namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests.RepositoryTests
{
    public class BasketItemRepositoryTest
    {
        private readonly BasketItemRepository _basketItemRepository;
        private readonly DBConfiguration _db;
        public BasketItemRepositoryTest()
        {
            _db= new DBConfiguration();
            _basketItemRepository = new BasketItemRepository(_db.context);

        }

        [Fact]
        public async Task AddAsync_AddingBasket_CreateBasketAndReturnTrue()
        {
            BasketItem basketItem = new() { Quantity=555 };
            var result = await _basketItemRepository.AddAsync(basketItem);
            await _db.context.SaveChangesAsync();

            BasketItem _basketItem = await _db.context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItem.Id);

            Assert.NotNull(_basketItem);
            Assert.Equal(_basketItem.Quantity, basketItem.Quantity);

            Assert.IsType<bool>(result);

            Assert.True(result);

        }

        [Fact]
        public async Task AddRangeAsync_AddingBaskets_CreateBasketsAndReturnTrue()
        {
            BasketItem basketItem1 = new() { Quantity = 444 };
            BasketItem basketItem2 = new() {Quantity = 222 };
          

            var beforeRecording = await _db.context.BasketItems.CountAsync();

            var result = await _basketItemRepository.AddRangeAsync(new() { basketItem1, basketItem2 });
            await _db.context.SaveChangesAsync();

            BasketItem _basketItem1 = await _db.context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItem1.Id);
            BasketItem _basketItem2 = await _db.context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItem2.Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.NotNull(basketItem1);
            Assert.NotNull(basketItem2);
            Assert.Equal(basketItem1.Quantity, _basketItem1.Quantity);
            Assert.Equal(basketItem2.Quantity, _basketItem2.Quantity);
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
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketItemRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal("Entity Bulunamadı!", ex.Message);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnBasket(bool tracking)
        {
            BasketItem basketItem =await _db.context.BasketItems.FirstOrDefaultAsync();

            var result = await _basketItemRepository.GetByIdAsync(basketItem.Id.ToString(), tracking);

            Assert.IsType<BasketItem>(result);
            Assert.NotNull(result);
            Assert.Equal(basketItem.Id, result.Id);
            Assert.Equal(basketItem.Quantity, result.Quantity);
        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveBasketAndReturnTrue()
        {
            BasketItem basketItem = await _db.context.BasketItems.FirstOrDefaultAsync();

            var result = _basketItemRepository.Remove(basketItem);
            await _db.context.SaveChangesAsync();

            var _basketItem = await _db.context.BasketItems.Where(x => x.Id == basketItem.Id).FirstOrDefaultAsync();

            Assert.Null(_basketItem);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _basketItemRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal("Entity Bulunamadı!", ex.Message);
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveBasketAndReturnTrue()
        {
            BasketItem basketItem =await  _db.context.BasketItems.FirstOrDefaultAsync();

            var result = await _basketItemRepository.RemoveByIdAsync(basketItem.Id.ToString());
            _db.context.SaveChanges();

            var _basket = await _db.context.BasketItems.FirstOrDefaultAsync(x => x.Id == basketItem.Id);

            Assert.True(result);
            Assert.Null(_basket);
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
