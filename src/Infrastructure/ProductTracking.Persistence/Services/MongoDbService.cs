using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.MongoDb;
using ProductTracking.Domain.Entities;
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
        private readonly IMapper _mapper;
        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings, IMapper mapper)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _basketCollection = database.GetCollection<BasketMongoDb>(mongoDbSettings.Value.BasketCollectionName);
            _mapper = mapper;
        }

        public async Task CreatAsync(Basket basket)
        {
            BasketMongoDb basketMongoDb = _mapper.Map<BasketMongoDb>(basket);
            if (basket.Category != null)
                basketMongoDb.CategoryName = basket.Category.Name;
            else
                basketMongoDb.CategoryName = "Çoklu Kategori";

            foreach (BasketItem basketItem in basket.BasketItems)
                basketMongoDb.BasketItems.Add(new() { ProductName=basketItem.Product.Name,Quantity=basketItem.Quantity,TotalPrice=basketItem.TotalPrice});
            

            await _basketCollection.InsertOneAsync(basketMongoDb);
            return;
        }

        public async Task<List<BasketMongoDb>> GetAsync()
            => await _basketCollection.Find(new BsonDocument()).ToListAsync();      
    }
}
