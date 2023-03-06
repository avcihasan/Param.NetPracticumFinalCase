using ProductTracking.Domain.Entities.MongoDbEntities;

namespace ProductTracking.Application.Abstractions.MongoDb
{
    public interface IMongoDbService
    {
        public Task CreatAsync(ProductTracking.Domain.Entities.Basket basket);
        public Task<List<BasketMongoDb>> GetAsync();
    }
}
