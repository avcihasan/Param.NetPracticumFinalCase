using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct;
using ProductTracking.Application.Features.Commands.ProductCommands.UpdateProduct;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using ProductTracking.Application.Features.Queries.ProductQueries.GetAllProducts;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class ProductsControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly ProductsController _productController;
        public ProductsControllerTest()
        {
            _mock = new Mock<IMediator>();
            _productController = new ProductsController(_mock.Object);
        }

        [Fact]
        public async Task GetAllProducts_ActionExecutes_ReturnObjectResultWithCategories()
        {

            _mock.Setup(m => m.Send(It.IsAny<GetAllProductsQueryRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new List<GetAllProductsQueryResponse>() { new(), new(), new() });


            var result = await _productController.GetAllProducts(new GetAllProductsQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetAllProductsQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var getAllCategoriesResponse = Assert.IsAssignableFrom<CustomResponseDto<List<GetAllProductsQueryResponse>>>(objectResult.Value);

            Assert.Null(getAllCategoriesResponse.Errors);
            Assert.NotEmpty(getAllCategoriesResponse.Data);
        }

        [Fact]
        public async Task GetByIdProduct_ActionExecutes_ReturnObjectResultWithCategory()
        {
            GetByIdProductQueryResponse response = new() { Id = Guid.NewGuid(), Name = "Test Kategori" };
            _mock.Setup(m => m.Send(It.IsAny<GetByIdProductQueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _productController.GetByIdProduct(new GetByIdProductQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetByIdProductQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(objectResult.StatusCode, 200);


            var getByIdCategoryResponse = Assert.IsAssignableFrom<CustomResponseDto<GetByIdProductQueryResponse>>(objectResult.Value);

            Assert.Null(getByIdCategoryResponse.Errors);
            Assert.NotNull(getByIdCategoryResponse.Data);
            Assert.Equal(response.Name, getByIdCategoryResponse.Data.Name);
        }



        [Fact]
        public async Task CreateProduct_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            _mock.Setup(m => m.Send(It.IsAny<CreateProductCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateProductCommandResponse());



            var result = await _productController.CreateProduct(It.IsAny<CreateProductCommandRequest>());

            _mock.Verify(x => x.Send(It.IsAny<CreateProductCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);

        }


        [Fact]
        public async Task UpdateProduct_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            UpdateProductCommandRequest request = new() { Id = Guid.NewGuid(), Name = "Update Ürün Test",UnitPrice=100 };


            _mock.Setup(m => m.Send(It.IsAny<UpdateProductCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UpdateProductCommandResponse());


            var result = await _productController.UpdateProduct(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public async Task RemoveProduct_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            RemoveProductCommandRequest request = new() {ProductId=Guid.NewGuid()};


            _mock.Setup(m => m.Send(It.IsAny<RemoveProductCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RemoveProductCommandResponse());


            var result = await _productController.RemoveProduct(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }
    }
}
