using ProductTracking.Application.Repositories;
using ProductTracking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IBasketRepository BasketRepository { get; }
        IGenericRepository<BasketItem> BasketItemRepository { get; }
        Task CommitAsync();

        void Commit();
    }
}
