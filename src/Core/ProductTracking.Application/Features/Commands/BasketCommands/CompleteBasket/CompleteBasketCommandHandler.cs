using MediatR;
using ProductTracking.Application.Abstractions.Basket;

namespace ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket
{
    public class CompleteBasketCommandHandler : IRequestHandler<CompleteBasketCommandRequest, CompleteBasketCommandResponse>
    {
        private readonly IBasketService _basketService;

        public CompleteBasketCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<CompleteBasketCommandResponse> Handle(CompleteBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await _basketService.CompleteBasketAsync(request.BasketId);
            return new();
        }
    }
}
