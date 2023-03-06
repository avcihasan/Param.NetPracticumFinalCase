using AutoMapper;
using Moq;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets;
using ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket;
using ProductTracking.Application.Mapping;
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
    public class SearchBasketQueryHandlerTest
    {
        private readonly Mock<IBasketService> _mock;
        private readonly SearchBasketQueryHandler _searchBasketQueryHandler;
        private readonly IMapper _mapper;

        public SearchBasketQueryHandlerTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProfile());
            });
            _mapper = mappingConfig.CreateMapper();
            _mock = new Mock<IBasketService>();
            _searchBasketQueryHandler = new SearchBasketQueryHandler( _mapper, _mock.Object  );
        }

        [Fact]
        public async Task Handle_ActionExecutes_ReturnListgetCompletedBaskets()
        {
            _mock.Setup(x => x.SearchBasketAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Basket>());

            var result = await _searchBasketQueryHandler.Handle(new SearchBasketQueryRequest(), It.IsAny<CancellationToken>());


            Assert.IsType<List<SearchBasketQueryResponse>>(result);

        }
    }
}
