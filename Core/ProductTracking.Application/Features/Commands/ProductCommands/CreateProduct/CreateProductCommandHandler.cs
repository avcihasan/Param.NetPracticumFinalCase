using AutoMapper;
using MediatR;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.ProductCommands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            bool result =await _unitOfWork.ProductRepository.AddAsync(_mapper.Map<Product>(request));
            if (!result)
                throw new Exception("Hata var!");
            
            await _unitOfWork.CommitAsync();

            return new();
        }
    }
}
