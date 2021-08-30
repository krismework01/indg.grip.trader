using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using INDG.GRIP.Trader.Application.Common.Behaviours;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Services;

namespace INDG.GRIP.Trader.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IJwtManager, JwtManager>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
        
        public static IMapperConfigurationExpression AddApplicationMaps(this IMapperConfigurationExpression cfg)
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
            return cfg;
        }

        public static Assembly GetApplicationAssembly(this IServiceCollection services)
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}