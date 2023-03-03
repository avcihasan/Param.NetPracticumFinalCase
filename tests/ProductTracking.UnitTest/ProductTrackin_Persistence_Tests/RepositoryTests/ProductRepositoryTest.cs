using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
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
    public class ProductRepositoryTest : DBConfiguration
    {
        private readonly ProductRepository _productRepository;
        public ProductRepositoryTest()
        {
            _productRepository = new ProductRepository(context);
        }

        [Theory]
        [InlineData("deneme",10)]
        [InlineData("deneme1",50)]
        [InlineData("deneme2",5)]
        public async Task AddAsync_AddingCategory_CreateProductAndReturnTrue(string name,decimal unitPrice)
        {
            Product product = new() { Name = name,CategoryId=context.Categories.First().Id,UnitPrice= unitPrice };
            var result = await _productRepository.AddAsync(product);
            await context.SaveChangesAsync();

            var productResult = context.Products.LastOrDefault();

            Assert.IsType<bool>(result);
            Assert.IsType<Product>(productResult);

            Assert.Equal(result, true);

            Assert.Equal(product.Name, productResult.Name);
        }

        [Theory]
        [InlineData("deneme0", "deneme3",10,10)]
        [InlineData("deneme1", "deneme4", 100, 50)]
        [InlineData("deneme2", "deneme5", 15, 10)]
        public async Task AddRangeAsync_AddingCategories_CreateProductsAndReturnTrue(string name1, string name2, decimal unitPrice1 , decimal unitPrice2)
        {
            List<Product> protucts = new()
            {
                new() { Name = name1 ,UnitPrice=unitPrice1,CategoryId=context.Categories.First().Id},
                new() { Name = name2,UnitPrice=unitPrice2,CategoryId=context.Categories.First().Id }
            };

            var beforeRecording = context.Products.Count();

            var result = await _productRepository.AddRangeAsync(protucts);
            await context.SaveChangesAsync();
            var afterRecording = context.Products.Count();

            var lastProduct = context.Products.LastOrDefault();

            Assert.Equal(lastProduct.Id, protucts.LastOrDefault().Id);
            Assert.IsType<bool>(result);
            Assert.Equal(beforeRecording + protucts.Count, afterRecording);

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
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _productRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnProduct(bool tracking)
        {
            var product = context.Products.FirstOrDefault();

            var result = await _productRepository.GetByIdAsync(product.Id.ToString(), tracking);

            Assert.IsType<Product>(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.CreatedDate, result.CreatedDate);


        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveProductAndReturnTrue()
        {
            var product = context.Products.FirstOrDefault();

            var result = _productRepository.Remove(product);
            await context.SaveChangesAsync();

            var newProduct = await context.Products.Where(x => x.Id == product.Id).FirstOrDefaultAsync();

            Assert.Null(newProduct);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _productRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal<string>(ex.Message, "Entity Bulunamadı!");

        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveProductAndReturnTrue()
        {
            var product = context.Products.FirstOrDefault();

            var result = await _productRepository.RemoveByIdAsync(product.Id.ToString());
            await context.SaveChangesAsync();

            var newProduct = context.Products.Where(x => x.Id == product.Id).FirstOrDefault();

            Assert.Equal(result, true);
            Assert.Null(newProduct);
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
