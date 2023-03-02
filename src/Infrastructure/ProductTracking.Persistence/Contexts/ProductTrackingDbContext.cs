using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductTracking.Domain.Entities;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Contexts
{
    public class ProductTrackingDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public ProductTrackingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BasketItem>()
            .HasOne(x  => x.Product)
            .WithMany(x => x.BasketItems)
            .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.ClientSetNull);
            

            base.OnModelCreating(builder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.Entity as BaseEntity != null)
                {
                    _ = data.State switch
                    {
                        EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                        _ => DateTime.Now
                    };
                }

            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
