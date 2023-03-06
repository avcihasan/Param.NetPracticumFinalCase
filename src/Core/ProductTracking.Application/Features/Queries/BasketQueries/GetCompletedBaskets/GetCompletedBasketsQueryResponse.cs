using ProductTracking.Domain.Entities.MongoDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Features.Queries.BasketQueries.GetCompletedBaskets
{
    public class GetCompletedBasketsQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }

#nullable enable
        public string? Description { get; set; }
#nullable disable
        public string CategoryName { get; set; }
        public decimal BasketTotalPrice { get; set; }

        public UserMongoDb User { get; set; } = new();



        public List<BasketItemMongoDb> BasketItems { get; set; } = new();
    }
}
