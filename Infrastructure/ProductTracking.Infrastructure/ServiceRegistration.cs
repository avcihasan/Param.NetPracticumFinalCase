using Microsoft.Extensions.DependencyInjection;

using ProductTracking.Application.Abstractions.Token;
using ProductTracking.Infrastructure.Services.Token;

namespace ProductTracking.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection service)
        {
            service.AddScoped<ITokenHandler,TokenHandler>();
        }
    }
}
