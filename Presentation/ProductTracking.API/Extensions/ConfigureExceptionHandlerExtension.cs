using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace ProductTracking.API.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        
        public static void ConfigureExceptionHandler<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError(contextFeature.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            Title = "Hata Alındı!",
                            Message = contextFeature.Error.Message
                            
                        }));

                    }
                });
            });
        }
    }
}
