using AutoMapper;
using MediatR;
using ProductTracking.Application.Abstractions.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets
{
    public class GetCompletedBasketsQueryHandler : IRequestHandler<GetCompletedBasketsQueryRequest, List<GetCompletedBasketsQueryResponse>>
    {

        private readonly IMongoDbService _mongoDbService;
        private readonly IMapper _mapper;
        public GetCompletedBasketsQueryHandler(IMongoDbService mongoDbService, IMapper mapper)
        {
            _mongoDbService = mongoDbService;
            _mapper = mapper;
        }

        public async Task<List<GetCompletedBasketsQueryResponse>> Handle(GetCompletedBasketsQueryRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<GetCompletedBasketsQueryResponse>>(await _mongoDbService.GetAsync());
        }
    }
}
