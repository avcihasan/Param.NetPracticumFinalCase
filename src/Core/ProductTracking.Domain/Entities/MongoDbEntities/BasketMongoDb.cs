using MongoDB.Bson.Serialization.Attributes;

namespace ProductTracking.Domain.Entities.MongoDbEntities
{
    public class BasketMongoDb
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
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
