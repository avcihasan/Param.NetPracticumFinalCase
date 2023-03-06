using Microsoft.EntityFrameworkCore;
using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(ProductTrackingDbContext context) : base(context)
        {
        }

        public async Task<Basket> GetBasketWithİtems(string basketId,bool tracking=true)
        {
            IQueryable<Basket> baskets = _dbSet.Include(x=>x.BasketItems).ThenInclude(x=>x.Product).ThenInclude(x=>x.Category).Include(x=>x.User).AsQueryable();
            if (!tracking)
                baskets = baskets.AsNoTracking();

            Basket basket = await baskets.FirstOrDefaultAsync(x => x.Id == Guid.Parse(basketId));
            if (baskets == null)
                throw new Exception("Basket Bulunamadı!");
            return basket;
        }

        public async Task<Basket> GetSingleBasketWithPropertiesAsync(Expression<Func<Basket, bool>> method, bool tracking = true)
        {
            IQueryable<Basket> baskets = _dbSet.Include(x => x.BasketItems).ThenInclude(x => x.Product).ThenInclude(x => x.Category).Include(x => x.User).AsQueryable();
            if (!tracking)
                baskets = _dbSet.AsNoTracking();
            Basket basket = await baskets.FirstOrDefaultAsync(method);
            if (basket == null)
                throw new Exception("Basket Bulunamadı!");
            return basket;
        }
    }
}
