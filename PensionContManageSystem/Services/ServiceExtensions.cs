using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Diagnostics;
using PensionContManageSystem.Domain.Entity;
using Serilog;

namespace PensionContManageSystem.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddHttpCacheHeaders(
                (experationOpt) =>
                {
                    experationOpt.MaxAge = 120;
                    experationOpt.CacheLocation = CacheLocation.Private;
                },
                (validateOpt) =>
                {
                    validateOpt.MustRevalidate = true;
                }
                );
        }
        public static void ConfigureGlobalExceptionHandler(this IApplicationBuilder app)//Global Error handling configuration
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    //context.Response.StatusCode = 500;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error($"Something went wrong in the {contextFeature.Error}");

                        await context.Response.WriteAsync(new GlobalErrorHandling
                        {
                            statusCode = context.Response.StatusCode,
                            message = "Internal Server Error. Please Try Again Later."
                        }.ToString());
                    }
                });
            });
        }
    }
}
