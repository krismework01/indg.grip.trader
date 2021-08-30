using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using INDG.GRIP.Trader.Domain.Aggregates.Products;

namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Status> Status { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}