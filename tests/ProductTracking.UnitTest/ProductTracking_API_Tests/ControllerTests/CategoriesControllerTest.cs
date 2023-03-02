using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.API.Controllers;
using ProductTracking.Application.DTOs.ResponseDTOs;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetAllCategories;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_API_Tests.ControllerTests
{
    public class CategoriesControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly CategoriesController _categoriesController;
        public CategoriesControllerTest()
        {
            _mock = new Mock<IMediator>();
            _categoriesController = new CategoriesController(_mock.Object);
        }

        [Fact]
        public async Task GetAllCategories_ActionExecutes_ReturnObjectResultWithCategories()
        {

            _mock.Setup(m => m.Send(It.IsAny<GetAllCategoriesQueryRequest>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new List<GetAllCategoriesQueryResponse>() { new(),new(),new()});


            var result = await _categoriesController.GetAllCategories(new GetAllCategoriesQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetAllCategoriesQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            var getAllCategoriesResponse = Assert.IsAssignableFrom<CustomResponseDto<List<GetAllCategoriesQueryResponse>>>(objectResult.Value);

            Assert.Null(getAllCategoriesResponse.Errors);
            Assert.NotEmpty(getAllCategoriesResponse.Data);    
        }

        [Fact]
        public async Task GetByIdCategory_ActionExecutes_ReturnObjectResultWithCategory()
        {
            GetByIdCategoryQueryResponse response = new() { Id = Guid.NewGuid(), Name = "Test Kategori" };
            _mock.Setup(m => m.Send(It.IsAny<GetByIdCategoryQueryRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _categoriesController.GetByIdCategory(new GetByIdCategoryQueryRequest());

            _mock.Verify(x => x.Send(It.IsAny<GetByIdCategoryQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(objectResult.StatusCode, 200);


            var getByIdCategoryResponse = Assert.IsAssignableFrom<CustomResponseDto<GetByIdCategoryQueryResponse>>(objectResult.Value);

            Assert.Null(getByIdCategoryResponse.Errors);
            Assert.NotNull(getByIdCategoryResponse.Data);
            Assert.Equal(response.Name,getByIdCategoryResponse.Data.Name);
        }



        [Fact]
        public async Task CreateCategory_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            _mock.Setup(m => m.Send(It.IsAny<CreateCategoryCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateCategoryCommandResponse());



            var result = await _categoriesController.CreateCategory(It.IsAny<CreateCategoryCommandRequest>());

            _mock.Verify(x => x.Send(It.IsAny<CreateCategoryCommandRequest>(), It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);

        }


        [Fact]
        public async Task UpdateCategory_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            UpdateCategoryCommandRequest request = new() { Id=Guid.NewGuid(),Name="Update Kategori Test" };


            _mock.Setup(m => m.Send(It.IsAny<UpdateCategoryCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new UpdateCategoryCommandResponse());


            var result = await _categoriesController.UpdateCategory(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public async Task RemoveCategory_ActionExecutes_ReturnObjectResultWithNoContentDto()
        {
            RemoveCategoryCommandRequest request = new() { CategoryId = Guid.NewGuid()};


            _mock.Setup(m => m.Send(It.IsAny<RemoveCategoryCommandRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RemoveCategoryCommandResponse());


            var result = await _categoriesController.RemoveCategory(request);

            _mock.Verify(x => x.Send(request, It.IsAny<CancellationToken>()), Times.Once);

            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Null(objectResult.Value);
            Assert.Equal(204, objectResult.StatusCode);
        }
    }
}
