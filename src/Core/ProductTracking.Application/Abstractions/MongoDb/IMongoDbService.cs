using ProductTracking.Domain.Entities.MongoDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.MongoDb
{
    public interface IMongoDbService
    {
        public Task CreatAsync(BasketMongoDb basket);
        public Task<List<BasketMongoDb>> GetAsync();
    }
}
