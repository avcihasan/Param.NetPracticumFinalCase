using MediatR;
using ProductTracking.Application.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory
{
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest, RemoveCategoryCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RemoveCategoryCommandResponse> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
           bool result=await _unitOfWork.CategoryRepository.RemoveByIdAsync(request.CategoryId.ToString());
            if (!result)
                throw new Exception("Hata");
            await _unitOfWork.CommitAsync();
            return new();
        }
    }
}
