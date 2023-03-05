using AutoMapper;
using Moq;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.CategoryCommandsTests
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        public CreateCategoryCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _createCategoryCommandHandler = new CreateCategoryCommandHandler(_mock.Object,_mapper);
        }


        [Theory]
        [InlineData("Test Kategori")]
        [InlineData("Test Kategori1")]
        [InlineData("Test Kategori2")]
        public async Task Handle_ActionThrowException_ReturnException(string name)
        {
            CreateCategoryCommandRequest request = new() { Name = name };
            _mock.Setup(x => x.CategoryRepository.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync(false);
            _mock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _createCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>()));


            _mock.Verify(x => x.CategoryRepository.AddAsync(It.IsAny<Category>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Never);
            Assert.Equal("hata", ex.Message);
                

        }

        [Theory]
        [InlineData("Test Kategori")]
        [InlineData("Test Kategori1")]
        [InlineData("Test Kategori2")]
        public async Task Handle_ActionExecutes_CreateCategoryAndReturnCreateCategoryCommandResponse(string name)
        {
            CreateCategoryCommandRequest request = new() { Name= name };
            _mock.Setup(x => x.CategoryRepository.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync(true);
            _mock.Setup(x => x.CommitAsync())
               .Returns(Task.CompletedTask);


            var result = await _createCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.CategoryRepository.AddAsync(It.IsAny<Category>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);

            Assert.IsType<CreateCategoryCommandResponse>(result);

        }
    }
}
