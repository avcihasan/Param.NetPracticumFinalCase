using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductTracking.Application.Features.Queries.BasketQueries.GetBasketItems;

namespace ProductTracking.Application.Features.Queries.BasketQueries.SearchBasket
{
    public class SearchBasketQueryResponse
    {
        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable
        public DateTime? CompletedDate { get; set; }
        public bool IsComplete { get; set; } = false;

        public Guid? CategoryId { get; set; }
        //public Category Category { get; set; }

        public ICollection<GetBasketItemsQueryResponse> BasketItems { get; set; }
    }
}
