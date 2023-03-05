using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Domain.Entities.MongoDbEntities;
using ProductTracking.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Services
{
    public class MongoDbService: IMongoDbService
    {
        private readonly IMongoCollection<BasketMongoDb> _basketCollection;
        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _basketCollection = database.GetCollection<BasketMongoDb>(mongoDbSettings.Value.BasketCollectionName);
        }

        public async Task CreatAsync(BasketMongoDb basket)
        {
            await _basketCollection.InsertOneAsync(basket);
            return;
        }

        public async Task<List<BasketMongoDb>> GetAsync()
            => await _basketCollection.Find(new BsonDocument()).ToListAsync();      
    }
}
