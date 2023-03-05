using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.CategoryCommandsTests
{
    public class RemoveCategoryCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly RemoveCategoryCommandHandler _removeCategoryCommandHandler;
        public RemoveCategoryCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            _removeCategoryCommandHandler = new RemoveCategoryCommandHandler(_mock.Object);
        }

        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionThrowException_ReturnException(Guid categorId)
        {
            RemoveCategoryCommandRequest request = new() { CategoryId= categorId };
            _mock.Setup(x => x.CategoryRepository.RemoveByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(false);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);


            Exception ex=await Assert.ThrowsAsync<Exception>(async ()=> await _removeCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>()));

            _mock.Verify(x=>x.CategoryRepository.RemoveByIdAsync(It.IsAny<string>()),Times.Once) ;
            _mock.Verify(x=>x.CommitAsync(),Times.Never);

            Assert.Equal("Hata", ex.Message);

        }


        [Theory]
        [InlineData( "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData( "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData( "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionExecutes_RemoveCategorAndReturnRemoveCategoryCommandResponse(Guid categorId)
        {
            RemoveCategoryCommandRequest request = new() { CategoryId = categorId };
            _mock.Setup(x => x.CategoryRepository.RemoveByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(true);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);

            var result = await _removeCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.CategoryRepository.RemoveByIdAsync(It.IsAny<string>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);

            Assert.IsType<RemoveCategoryCommandResponse>(result);

        }
    }
}   
