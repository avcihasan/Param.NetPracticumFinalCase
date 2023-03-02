using MediatR;
using ProductTracking.Application.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest, RemoveProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfwork;

        public RemoveProductCommandHandler(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
           bool result= await _unitOfwork.ProductRepository.RemoveByIdAsync(request.ProductId.ToString());
            if (!result)
                throw new Exception("Hata!");

            await _unitOfwork.CommitAsync();
            return new();

        }
    }
}
