using ProductTracking.Application.Abstractions.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Contexts
{
    public class MongoDbSettings 
    {
        public string BasketCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
