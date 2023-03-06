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
    public class CategoryRepositoryTest
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly DBConfiguration _db;
        public CategoryRepositoryTest()
        {
            _db=new DBConfiguration();
            _categoryRepository = new CategoryRepository(_db.context);
        }

        [Theory]
        [InlineData("deneme")]
        [InlineData("deneme1")]
        [InlineData("deneme2")]
        public async Task AddAsync_AddingCategory_CreateCategoryAndReturnTrue(string name)
        {
            Category category = new() { Name = name };
            var result = await _categoryRepository.AddAsync(category);
            await _db.context.SaveChangesAsync();

            var categoryResult = await _db.context.Categories.FirstOrDefaultAsync(x=>x.Name==name);

            Assert.IsType<bool>(result);
            Assert.IsType<Category>(categoryResult);

            Assert.True(result);
            Assert.NotNull(categoryResult);     
            Assert.Equal(category.Name, categoryResult.Name);
        }

        [Theory]
        [InlineData("deneme0", "deneme3")]
        [InlineData("deneme1", "deneme4")]
        [InlineData("deneme2", "deneme5")]
        public async Task AddRangeAsync_AddingCategories_CreateCategoriesAndReturnTrue(string name1, string name2)
        {
            Category category1 = new() {Id=Guid.NewGuid(), Name = name1 };
            Category category2 = new() { Id = Guid.NewGuid(), Name = name2 };

          


            var result = await _categoryRepository.AddRangeAsync(new() { category1, category2 });
            await _db.context.SaveChangesAsync();

            Category _category1 =await _db.context.Categories.FirstOrDefaultAsync(x => x.Id == category1.Id);
            Category _category2 = await _db.context.Categories.FirstOrDefaultAsync(x => x.Id == category2.Id);

            Assert.Equal(_category1.Id, category1.Id);
            Assert.Equal(_category2.Id, category2.Id);
            Assert.Equal(_category2.Name, category2.Name);
            Assert.Equal(_category1.Name, category1.Name);
            Assert.IsType<bool>(result);

            Assert.True(result);

        }


        //[Theory]
        //[InlineData(true)]
        //[InlineData(false)]
        //public void GetAll_ActionExecutes_ReturnCategories(bool tracking)
        //{
        //    var result = _categoryRepository.GetAll(tracking);

        //    Assert.IsType<IQueryable<Category>>(result);


        //}

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_InvalidId_ReturnException(bool tracking)
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _categoryRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal("Entity Bulunamadı!", ex.Message);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnCategory(bool tracking)
        {
            Category category =await _db.context.Categories.FirstOrDefaultAsync();     

            var result = await _categoryRepository.GetByIdAsync(category.Id.ToString(), tracking);

            Assert.IsType<Category>(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.CreatedDate, result.CreatedDate);
        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveCategoryAndReturnTrue()
        {
            var category =await _db.context.Categories.FirstOrDefaultAsync();

            var result = _categoryRepository.Remove(category);
            await _db.context.SaveChangesAsync();

            var newCategory = await _db.context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            Assert.Null(newCategory);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _categoryRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal("Entity Bulunamadı!", ex.Message);
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveCategoryAndReturnTrue()
        {
            Category category =await _db.context.Categories.FirstOrDefaultAsync();


            var result = await _categoryRepository.RemoveByIdAsync(category.Id.ToString());
            await _db.context.SaveChangesAsync();

            var newCategory = await _db.context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            Assert.True(result);
            Assert.Null(newCategory);
        }

        //[Fact]
        //public async Task Update_ActionExecutes_UpdateCategoryAndReturnTrue()
        //{
        //    var category = context.Categories.FirstOrDefault().Id;

        //    Category updateCategory = new() { Id = category, Name = "Test Kategori Update" };


        //    var result = _categoryRepository.Update(updateCategory);
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
