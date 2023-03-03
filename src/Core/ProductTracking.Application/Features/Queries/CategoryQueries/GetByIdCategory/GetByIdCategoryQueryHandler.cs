using AutoMapper;
using MediatR;
using ProductTracking.Application.UnitOfWorks;

namespace ProductTracking.Application.Features.Queries.CategoryQueries.GetByIdCategory
{
    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var xxX = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId.ToString());

            return _mapper.Map<GetByIdCategoryQueryResponse>(xxX);
        }

    }
}
