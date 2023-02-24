using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Repositories
{
    public class ProductListRepository : GenericRepository<ProductList>, IProductListRepository
    {
        public ProductListRepository(ProductTrackingDbContext context) : base(context)
        {
        }
    }
}
