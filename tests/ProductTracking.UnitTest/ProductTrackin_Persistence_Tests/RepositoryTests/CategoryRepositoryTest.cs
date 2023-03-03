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
    public class CategoryRepositoryTest:DBConfiguration
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryTest()
        {
            _categoryRepository = new CategoryRepository(context);
        }

        [Theory]
        [InlineData("deneme")]
        [InlineData("deneme1")]
        [InlineData("deneme2")]
        public async Task AddAsync_AddingCategory_CreateCategoryAndReturnTrue(string name)
        {
            Category category = new() { Name = name };
            var result = await _categoryRepository.AddAsync(category);
            await context.SaveChangesAsync();

            var categoryResult = context.Categories.LastOrDefault();

            Assert.IsType<bool>(result);
            Assert.IsType<Category>(categoryResult);

            Assert.Equal(result, true);

            Assert.Equal(category.Name, categoryResult.Name);
        }

        [Theory]
        [InlineData("deneme0", "deneme3")]
        [InlineData("deneme1", "deneme4")]
        [InlineData("deneme2", "deneme5")]
        public async Task AddRangeAsync_AddingCategories_CreateCategoriesAndReturnTrue(string name1, string name2)
        {
            List<Category> categories = new()
            {
                new() { Name = name1 },
                new() { Name = name2 }
            };

            var beforeRecording = context.Categories.Count();

            var result = await _categoryRepository.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var afterRecording = context.Categories.Count();

            var lastCategory = context.Categories.LastOrDefault();

            Assert.Equal(lastCategory.Id, categories.LastOrDefault().Id);
            Assert.IsType<bool>(result);
            Assert.Equal(beforeRecording + categories.Count, afterRecording);

            Assert.Equal(result, true);

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
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnCategory(bool tracking)
        {
            var category = context.Categories.FirstOrDefault();

            var result = await _categoryRepository.GetByIdAsync(category.Id.ToString(), tracking);

            Assert.IsType<Category>(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.CreatedDate, result.CreatedDate);
        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveCategoryAndReturnTrue()
        {
            var category = context.Categories.FirstOrDefault();

            var result = _categoryRepository.Remove(category);
            await context.SaveChangesAsync();

            var newCategory = await context.Categories.Where(x => x.Id == category.Id).FirstOrDefaultAsync();

            Assert.Null(newCategory);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _categoryRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");
        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveCategoryAndReturnTrue()
        {
            var category = context.Categories.FirstOrDefault();

            var result = await _categoryRepository.RemoveByIdAsync(category.Id.ToString());
            await context.SaveChangesAsync();

            var newCategory = context.Categories.Where(x => x.Id == category.Id).FirstOrDefault();

            Assert.Equal(result, true);
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
