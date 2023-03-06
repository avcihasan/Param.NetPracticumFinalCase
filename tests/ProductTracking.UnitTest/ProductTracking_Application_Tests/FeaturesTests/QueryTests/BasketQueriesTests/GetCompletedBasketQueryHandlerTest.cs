using AutoMapper;
using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;
using ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets;
using ProductTracking.Application.Mapping;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.MongoDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTracking.UnitTest.ProductTracking_Application_Tests.FeaturesTests.QueryTests.BasketQueriesTests
{
    public class GetCompletedBasketQueryHandlerTest
    {
        private readonly Mock<IMongoDbService> _mock;
        private readonly GetCompletedBasketsQueryHandler _getCompletedBasketsQueryHandler;
        private readonly IMapper _mapper;

        public GetCompletedBasketQueryHandlerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _mock = new Mock<IMongoDbService>();
            _getCompletedBasketsQueryHandler = new GetCompletedBasketsQueryHandler(_mock.Object,_mapper);
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnListgetCompletedBaskets()
        {
            _mock.Setup(x => x.GetAsync())
                .ReturnsAsync(new List<BasketMongoDb>());
          
            var result = await _getCompletedBasketsQueryHandler.Handle(It.IsAny<GetCompletedBasketsQueryRequest>(), It.IsAny<CancellationToken>());

            _mock.Verify(x => x.GetAsync(), Times.Once);

            Assert.IsType<List<GetCompletedBasketsQueryResponse>>(result);

        }
    }
}
