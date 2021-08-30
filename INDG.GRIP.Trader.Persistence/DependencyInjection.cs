using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using INDG.GRIP.Trader.Persistence.Repository;

namespace INDG.GRIP.Trader.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceData(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TraderDbContext>((serviceProvider, options) =>
            {
                options
                    .EnableDetailedErrors()
                    .UseNpgsql(
                        configuration.GetSection("ConnectionStrings:PostgresConnection:connectionString").Value
                        + $"Database={configuration.GetSection("ConnectionStrings:PostgresConnection:Database").Value};");
            });
            services.AddScoped<IDbContext>(provider => provider.GetService<TraderDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
    }
}