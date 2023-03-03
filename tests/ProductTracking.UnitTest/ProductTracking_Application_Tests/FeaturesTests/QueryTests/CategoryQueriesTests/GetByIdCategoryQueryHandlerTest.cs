using AutoMapper;
using Moq;
using ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.QueryTests.CategoryQueriesTests
{
    public class GetByIdCategoryQueryHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly GetByIdCategoryQueryHandler _getByIdCategoryQueryHandler;
        public GetByIdCategoryQueryHandlerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mock = new Mock<IUnitOfWork>();
            _getByIdCategoryQueryHandler = new GetByIdCategoryQueryHandler(_mock.Object, _mapper);
        }


        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c5")]
        public async Task Handle_ActionExecutes_ReturnGetByIdCategoryQueryResponseWithCategory(Guid categoryId)
        {
            GetByIdCategoryQueryRequest request = new() { CategoryId = categoryId };
            _mock.Setup(x => x.CategoryRepository.GetByIdAsync(It.IsAny<string>(), true))
                .ReturnsAsync(new Category());


            var result = await _getByIdCategoryQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.CategoryRepository.GetByIdAsync(It.IsAny<string>(), true), Times.Once);

            Assert.IsType<GetByIdCategoryQueryResponse>(result);
        }

    }
}
