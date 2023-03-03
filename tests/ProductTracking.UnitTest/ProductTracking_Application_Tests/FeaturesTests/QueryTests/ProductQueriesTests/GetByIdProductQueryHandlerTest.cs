using AutoMapper;
using Moq;
using ProductTracking.Application.Features.Queries.ProductQueries.GetByIdProduct;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.QueryTests.ProductQueriesTests
{
    public class GetByIdProductQueryHandlerTest
    {
        private readonly Mock<IUnitOfWork> _mock;
        private readonly IMapper _mapper;
        private readonly GetByIdProductQueryHandler _getByIdProductQueryHandler;
        public GetByIdProductQueryHandlerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _mock = new Mock<IUnitOfWork>();
            _getByIdProductQueryHandler = new GetByIdProductQueryHandler(_mock.Object, _mapper);
        }


        [Theory]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c1")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c2")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c3")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c4")]
        [InlineData("0bf4b8d1-9516-4bea-9446-6db8ca2ad1c5")]
        public async Task Handle_ActionExecutes_ReturnGetByIdProductQueryResponseWithProduct(Guid productId)
        {
            GetByIdProductQueryRequest request = new() { ProductId = productId };
            _mock.Setup(x => x.ProductRepository.GetByIdAsync(It.IsAny<string>(), true))
                .ReturnsAsync(new Product());


            var result = await _getByIdProductQueryHandler.Handle(request, It.IsAny<CancellationToken>());

            _mock.Verify(x => x.ProductRepository.GetByIdAsync(It.IsAny<string>(), true), Times.Once);

            Assert.IsType<GetByIdProductQueryResponse>(result);

        }
    }
}
