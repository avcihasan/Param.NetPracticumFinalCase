using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
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
    public class ProductRepositoryTest 
    {
        private readonly ProductRepository _productRepository;
        private readonly DBConfiguration _db;
        public ProductRepositoryTest()
        {
            _db = new DBConfiguration();
            _productRepository = new ProductRepository(_db.context);
        }

        [Theory]
        [InlineData("deneme", 10)]
        [InlineData("deneme1", 50)]
        [InlineData("deneme2", 5)]
        public async Task AddAsync_AddingCategory_CreateProductAndReturnTrue(string name, decimal unitPrice)
        {
            Product product = new() {  Name = name, UnitPrice = unitPrice };
            var result = await _productRepository.AddAsync(product);
            await _db.context.SaveChangesAsync();

            var productResult = await _db.context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            Assert.IsType<bool>(result);
            Assert.IsType<Product>(productResult);

            Assert.True(result);
            Assert.NotNull(productResult);
            Assert.Equal(product.Name, productResult.Name);

        }

        [Theory]
        [InlineData("deneme0", "deneme3")]
        [InlineData("deneme1", "deneme4")]
        [InlineData("deneme2", "deneme5")]
        public async Task AddRangeAsync_AddingProducts_CreateProductsAndReturnTrue(string name1, string name2)
        {
            Product product1 = new() {Name = name1 };
            Product product2 = new() { Name = name2 };


            var result = await _productRepository.AddRangeAsync(new() { product1,product2 });
            await _db.context.SaveChangesAsync();
            var afterRecording = _db.context.Products.Count();

            Product _product1 = await _db.context.Products.FirstOrDefaultAsync(x=>x.Id==product1.Id);
            Product _product2 = await _db.context.Products.FirstOrDefaultAsync(x => x.Id == product2.Id);

            Assert.Equal(product1.Id, _product1.Id);
            Assert.Equal(product2.Id, _product2.Id);
            Assert.Equal(product2.Name, _product2.Name);
            Assert.Equal(product1.Name, _product1.Name);
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
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _productRepository.GetByIdAsync(Guid.NewGuid().ToString(), tracking));
            Assert.Equal("Entity Bulunamadı!", ex.Message);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetByIdAsync_ValidId_ReturnProduct(bool tracking)
        {
            Product product =await _db.context.Products.FirstOrDefaultAsync();

            var result = await _productRepository.GetByIdAsync(product.Id.ToString(), tracking);

            Assert.IsType<Product>(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.UnitPrice, result.UnitPrice);


        }

        [Fact]
        public async Task Remove_ActionExecutes_RemoveProductAndReturnTrue()
        {
            var product = await _db.context.Products.FirstOrDefaultAsync();

            var result = _productRepository.Remove(product);
            await _db.context.SaveChangesAsync();

            var _product = await _db.context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            Assert.Null(_product);
        }



        [Fact]
        public async Task RemoveByIdAsync_InvalidId_ReturnException()
        {

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _productRepository.RemoveByIdAsync(Guid.NewGuid().ToString()));
            Assert.Equal("Entity Bulunamadı!", ex.Message);

        }

        [Fact]
        public async Task RemoveByIdAsync_ValidId_RemoveProductAndReturnTrue()
        {
            var product = await _db.context.Products.FirstOrDefaultAsync();

            var result = await _productRepository.RemoveByIdAsync(product.Id.ToString());
            await _db.context.SaveChangesAsync();

            var newProduct = await _db.context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            Assert.True(result);
            Assert.Null(newProduct);

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
}