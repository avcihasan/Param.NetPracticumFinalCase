using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.BasketCommands.CompleteBasket
{
    public class CompleteBasketCommandRequest:IRequest<CompleteBasketCommandResponse>
    {
        public Guid BasketId { get; set; }
    }
}
