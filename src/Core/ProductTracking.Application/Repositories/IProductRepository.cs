using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {

    }
}
