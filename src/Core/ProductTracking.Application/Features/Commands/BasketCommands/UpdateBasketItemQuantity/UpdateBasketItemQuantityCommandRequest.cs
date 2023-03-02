using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.BasketCommands.UpdateBasketItemQuantity
{
    public class UpdateBasketItemQuantityCommandRequest:IRequest<UpdateBasketItemQuantityCommandResponse>
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
