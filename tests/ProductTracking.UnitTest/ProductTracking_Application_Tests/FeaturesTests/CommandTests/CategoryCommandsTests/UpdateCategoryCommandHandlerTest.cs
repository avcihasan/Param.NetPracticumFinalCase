using AutoMapper;
using Moq;
using ProductTracking.Application.Features.Commands.CategoryCommands.CreateCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory;
using ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.CommandTests.CategoryCommandsTests
{
    public class UpdateCategoryCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        public UpdateCategoryCommandHandlerTest()
        {
            _mock = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _updateCategoryCommandHandler = new UpdateCategoryCommandHandler(_mock.Object, _mapper);
        }

        [Theory]
        [InlineData("Test Kategori", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("Test Kategori1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("Test Kategori2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionThrowException_ReturnException(string name, Guid id)
        {
            UpdateCategoryCommandRequest request = new() { Name = name, Id = id };
            _mock.Setup(x => x.CategoryRepository.Update(It.IsAny<Category>()))
                .Returns(false);
            _mock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask);


            Exception ex=await Assert.ThrowsAsync<Exception>(async () =>await _updateCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>()));

            _mock.Verify(x => x.CategoryRepository.Update(It.IsAny<Category>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Never);

            Assert.Equal<string>(ex.Message, "Hata");

        }


        [Theory]
        [InlineData("Test Kategori", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("Test Kategori1", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("Test Kategori2", "0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        public async Task Handle_ActionExecutes_UpdateCategoryAndReturnUpdateCategoryCommandResponse(string name, Guid id)
        {
            UpdateCategoryCommandRequest request = new() { Name = name, Id = id };

            _mock.Setup(x => x.CategoryRepository.Update(It.IsAny<Category>()))
                .Returns(true);
            _mock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);

            var result = await _updateCategoryCommandHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.CategoryRepository.Update(It.IsAny<Category>()), Times.Once);
            _mock.Verify(x => x.CommitAsync(), Times.Once);
            Assert.IsType<UpdateCategoryCommandResponse>(result);
        }
    }
}
