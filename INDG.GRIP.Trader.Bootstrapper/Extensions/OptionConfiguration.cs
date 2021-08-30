using INDG.GRIP.Trader.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace INDG.GRIP.Trader.Bootstrapper.Extensions
{
    internal static class OptionConfiguration
    {
        internal static IServiceCollection AddOptionsExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();

            var auth = config.GetSection("Auth").Get<AuthConfig>();
            services.Configure<AuthConfig>(opt =>
            {
                opt.Audience = auth.Audience;
                opt.Issuer = auth.Issuer;
                opt.Key = auth.Key;
                opt.Lifetime = auth.Lifetime;
            });

            return services;
        }
    }
}