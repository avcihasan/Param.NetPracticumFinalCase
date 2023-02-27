using AutoMapper;
using MediatR;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;

namespace ProductTracking.Application.Features.Commands.CategoryCommands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
           bool result= _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(request));
            if (!result)
                throw new Exception("Hata");
            await _unitOfWork.CommitAsync();
            return new();
            
        }
    }
}
