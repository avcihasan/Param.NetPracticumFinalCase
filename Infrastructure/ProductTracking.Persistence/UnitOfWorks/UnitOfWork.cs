using ProductTracking.Application.Repositories;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Contexts;
using ProductTracking.Persistence.Repositories;

namespace ProductTracking.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductTrackingDbContext _context;
        public IGenericRepository<Product> ProductRepository { get; private set; }
        public UnitOfWork(ProductTrackingDbContext context)
        {
            _context = context;
            ProductRepository = new GenericRepository<Product>(_context);
        }


        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
