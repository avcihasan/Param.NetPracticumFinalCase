using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Repositories
{
    public interface IBasketRepository:IGenericRepository<Basket>
    {
        public Task<Basket> GetBasketWithİtems(string basketId,bool tracking=true);
    }
}
