﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.ProductCommands.RemoveProduct
{
    public class RemoveProductCommandRequest:IRequest<RemoveProductCommandResponse>
    {
        public Guid ProductId { get; set; }
    }
}
