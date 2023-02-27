using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Commands.CategoryCommands.RemoveCategory
{
    public class RemoveCategoryCommandRequest:IRequest<RemoveCategoryCommandResponse>
    {
        public Guid CategoryId { get; set; }
    }
}
