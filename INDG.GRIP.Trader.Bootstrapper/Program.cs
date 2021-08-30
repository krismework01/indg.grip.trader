using INDG.GRIP.Trader.Application.Logic.System;
using INDG.GRIP.Trader.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Bootstrapper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            DatabaseMigrate(host);
            await SeedData(host);
            await host.RunAsync();
        }

        private static void DatabaseMigrate(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TraderDbContext>();
                    logger.LogInformation($"Database: {context.Database.GetDbConnection().Database}");
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static async Task SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
                await mediator.Send(new SeedCommand(), CancellationToken.None);
            }
        }
    }
}
