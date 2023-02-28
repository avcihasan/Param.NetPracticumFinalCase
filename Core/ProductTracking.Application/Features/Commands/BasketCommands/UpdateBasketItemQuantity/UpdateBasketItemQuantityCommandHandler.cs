using AutoMapper;
using MediatR;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.DTOs.BasketItemDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandHandler : IRequestHandler<UpdateBasketItemQuantityCommandRequest, UpdateBasketItemQuantityCommandResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public UpdateBasketItemQuantityCommandHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<UpdateBasketItemQuantityCommandResponse> Handle(UpdateBasketItemQuantityCommandRequest request, CancellationToken cancellationToken)
        {
           await _basketService.UpdateQuantityAsync(_mapper.Map<UpdateBasketItemDto>(request));
            return new();
        }
    }
}
