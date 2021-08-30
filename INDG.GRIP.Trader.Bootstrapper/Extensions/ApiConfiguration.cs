using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace INDG.GRIP.Trader.Bootstrapper.Extensions
{
    internal static class ApiConfiguration
    {
        internal static IServiceCollection AddVersionConfiguration(this IServiceCollection services)
        {
        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });

            return services;
        }
    }
}