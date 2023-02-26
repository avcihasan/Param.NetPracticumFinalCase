using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductTracking.Application.Abstractions.Basket;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Application.UnitOfWorks;
using ProductTracking.Domain.Entities.Identity;
using ProductTracking.Persistence.Contexts;
using ProductTracking.Persistence.Services;
using ProductTracking.Persistence.UnitOfWorks;

namespace ProductTracking.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection service)
        {
            service.AddDbContext<ProductTrackingDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            service.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ProductTrackingDbContext>()
            .AddDefaultTokenProviders();

            service.AddScoped<IUnitOfWork,UnitOfWork>();
            service.AddScoped<IBasketService, BasketService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthService, AuthService>();
        }
    }
}
