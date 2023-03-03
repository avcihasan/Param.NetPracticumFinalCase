using AutoMapper;
using Moq;
using ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.ProductCommandsTests
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly CreateProductCommandHandler _createProductCommandHandler;
        public CreateProductCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _createProductCommandHandler = new CreateProductCommandHandler(_mock.Object, _mapper);
        }


        [Theory]
        [InlineData("Test Ürün İsmi", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2", 10)]
        [InlineData("Test Ürün İsmi1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3", 20)]
        [InlineData("Test Ürün İsmi2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4", 40)]
        public async Task Handle_ActionThrowException_ReturnException(string name, Guid categoryId, decimal unitPrice)
        {
            CreateProductCommandRequest request = new() { Name = name, CategoryId = categoryId, UnitPrice = unitPrice };
            _mock.Setup(x => x.ProductRepository.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(false);
            _mock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _createProductCommandHandler.Handle(request, It.IsAny<CancellationToken>()));


            _mock.Verify(x => x.ProductRepository.AddAsync(It.IsAny<Product>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Never);
            Assert.Equal<string>(ex.Message, "Hata var!");


        }

        [Theory]
        [InlineData("Test Ürün İsmi", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2", 10)]
        [InlineData("Test Ürün İsmi1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3", 20)]
        [InlineData("Test Ürün İsmi2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4", 40)]
        public async Task Handle_ActionExecutes_CreateProductAndReturnCreateProductCommandResponse(string name, Guid categoryId, decimal unitPrice)
        {
            CreateProductCommandRequest request = new() { Name = name, CategoryId = categoryId, UnitPrice = unitPrice };
            _mock.Setup(x => x.ProductRepository.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(true);
            _mock.Setup(x => x.CommitAsync())
               .Returns(Task.CompletedTask);


            var result = await _createProductCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.ProductRepository.AddAsync(It.IsAny<Product>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);

            Assert.IsType<CreateProductCommandResponse>(result);

        }
    }
}
