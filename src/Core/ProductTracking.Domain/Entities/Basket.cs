using MongoDB.Bson.Serialization.Attributes;
using ProductTracking.Domain.Entities.Identity;

namespace ProductTracking.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Basket:BaseEntity
    {

        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable
        public DateTime? CompletedDate { get; set; }
        public bool IsComplete { get; set; } = false;
        public decimal BasketTotalPrice { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
