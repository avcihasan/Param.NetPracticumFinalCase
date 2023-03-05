using Moq;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct;
using ProductTracking.Application.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.ProductCommandsTests
{
    public class RemoveProductCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly RemoveProductCommandHandler _removeProductCommandHandler;
        public RemoveProductCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            _removeProductCommandHandler = new RemoveProductCommandHandler(_mock.Object);
        }

        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionThrowException_ReturnException(Guid productId)
        {
            RemoveProductCommandRequest request = new() { ProductId = productId };
            _mock.Setup(x => x.ProductRepository.RemoveByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);


            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _removeProductCommandHandler.Handle(request, It.IsAny<CancellationToken>()));

            _mock.Verify(x => x.ProductRepository.RemoveByIdAsync(It.IsAny<string>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal("Hata!",ex.Message );

        }


        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionExecutes_RemoveProductAndReturnRemoveProductCommandResponse(Guid productId)
        {
            RemoveProductCommandRequest request = new() { ProductId = productId };
            _mock.Setup(x => x.ProductRepository.RemoveByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);

            var result = await _removeProductCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.ProductRepository.RemoveByIdAsync(It.IsAny<string>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);

            Assert.IsType<RemoveProductCommandResponse>(result);

        }
    }
}
