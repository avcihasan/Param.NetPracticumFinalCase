using AutoMapper;
using Moq;
using ProductTracking.Application.Features.Commands.ProductCommands.UpdateProduct;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.ProductCommandsTests
{
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly UpdateProductCommandHandler _updateProductCommandHandler;
        public UpdateProductCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _updateProductCommandHandler = new UpdateProductCommandHandler(_mock.Object, _mapper);
        }

        [Theory]
        [InlineData("Test Ürün İsmi", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2", 10)]
        [InlineData("Test Ürün İsmi1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4", 50)]
        [InlineData("Test Ürün İsmi2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c5", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c6", 200)]
        public async Task Handle_ActionThrowException_ReturnException(string name, Guid id, Guid categoryId, decimal unitPrice)
        {
            UpdateProductCommandRequest request = new() 
            { 
                Name = name, 
                Id = id, 
                UnitPrice = unitPrice, 
                CategoryId = categoryId 
            };
            _mock.Setup(x => x.ProductRepository.Update(It.IsAny<Product>()))
                .Returns(false);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);


            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _updateProductCommandHandler.Handle(request, It.IsAny<CancellationToken>()));

            _mock.Verify(x => x.ProductRepository.Update(It.IsAny<Product>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal<string>(ex.Message, "Hata var");

        }


        [Theory]
        [InlineData("Test Ürün İsmi", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2", 10)]
        [InlineData("Test Ürün İsmi1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4", 50)]
        [InlineData("Test Ürün İsmi2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c5", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c6", 200)]
        public async Task Handle_ActionExecutes_UpdateProductAndReturnUpdateProductCommandResponse(string name, Guid id, Guid categoryId, decimal unitPrice)
        {
            UpdateProductCommandRequest request = new()
            {
                Name = name,
                Id = id,
                UnitPrice = unitPrice,
                CategoryId = categoryId
            };

            _mock.Setup(x => x.ProductRepository.Update(It.IsAny<Product>()))
                .Returns(true);
            _mock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

            var result = await _updateProductCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.ProductRepository.Update(It.IsAny<Product>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);
            Assert.IsType<UpdateProductCommandResponse>(result);


        }
    }
}
