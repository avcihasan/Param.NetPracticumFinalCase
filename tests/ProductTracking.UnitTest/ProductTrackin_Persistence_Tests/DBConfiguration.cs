using Microsoft.EntityFrameworkCore;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Domain.Entities;
using ProductTracking.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.UnitTest.ProductTrackin_Persistence_Tests
{
    public class DBConfiguration
    {
        protected DbContextOptions<ProductTrackingDbContext> _contextOptions { get; set; }
        public ProductTrackingDbContext context { get; set; }
        public DBConfiguration()
        {
            _contextOptions = new DbContextOptionsBuilder<ProductTrackingDbContext>().UseInMemoryDatabase("ornek db").Options;
            context = new ProductTrackingDbContext(_contextOptions);
            Seed();

        }

        public void Seed()
        {
            using (var context = new ProductTrackingDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Categories.AddRange(
                    new Category { Name = "Kategori 1", CreatedDate = DateTime.Now },
                    new Category { Name = "Kategori 2", CreatedDate = DateTime.Now },
                     new Category { Name = "Kategori 3", CreatedDate = DateTime.Now },
                      new Category { Name = "Kategori 4", CreatedDate = DateTime.Now },
                      new Category { Name = "Kategori 5", CreatedDate = DateTime.Now });

                context.Products.AddRange(
                    new Product { Name = "Ürün 1", CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UnitPrice = 10 },
                    new Product { Name = "Ürün 2", CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UnitPrice = 10 },
                    new Product { Name = "Ürün 3", CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UnitPrice = 10 },
                    new Product { Name = "Ürün 4", CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UnitPrice = 10 },
                    new Product { Name = "Ürün 5", CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UnitPrice = 10 }
                    );

                context.BasketItems.AddRange(
                    new BasketItem { BasketId = Guid.NewGuid(), CreatedDate = DateTime.Now, ProductId = Guid.NewGuid(), Quantity = 10 },
                     new BasketItem { BasketId = Guid.NewGuid(), CreatedDate = DateTime.Now, ProductId = Guid.NewGuid(), Quantity = 10 },
                     new BasketItem { BasketId = Guid.NewGuid(), CreatedDate = DateTime.Now, ProductId = Guid.NewGuid(), Quantity = 10 },
                     new BasketItem { BasketId = Guid.NewGuid(), CreatedDate = DateTime.Now, ProductId = Guid.NewGuid(), Quantity = 10 },
                     new BasketItem { BasketId = Guid.NewGuid(), CreatedDate = DateTime.Now, ProductId = Guid.NewGuid(), Quantity = 10 });

                context.Baskets.AddRange(
                    new Basket { CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UserId = Guid.NewGuid().ToString() },
                    new Basket { CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UserId = Guid.NewGuid().ToString() },
                    new Basket { CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UserId = Guid.NewGuid().ToString() },
                    new Basket { CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UserId = Guid.NewGuid().ToString() },
                    new Basket { CategoryId = Guid.NewGuid(), CreatedDate = DateTime.Now, UserId = Guid.NewGuid().ToString() });

                context.Users.AddRange(
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "hsn@gmail.com",
                        Name = "Hasan",
                        Surname = "Avcı",
                        UserName = "hasan"
                    },
                    new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = "deneme@gmail.com",
                        Name = "Deneme",
                        Surname = "Deneme",
                        UserName = "deneme"
                    });

            context.SaveChanges();

            }
        }
    }
}
