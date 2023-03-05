using MongoDB.Bson.Serialization.Attributes;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Domain.Entities.MongoDbEntities
{
    public class BasketMongoDb
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
#nullable enable
        public string? Description { get; set; }
#nullable disable
        public DateTime CompletedDate { get; set; }
        public bool IsComplete { get; set; }

        public string UserId { get; set; }
        public UserMongoDb User { get; set; } = new();

        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<BasketItemMongoDb> BasketItems { get; set; }
    }
}
