using Microsoft.EntityFrameworkCore;

namespace INDG.GRIP.Trader.Persistence
{
    public class TraderDbContextFactory : DesignTimeDbContextFactoryBase<TraderDbContext>
    {
        protected override TraderDbContext CreateNewInstance(DbContextOptions<TraderDbContext> options)
            => new TraderDbContext(options);
    }
}