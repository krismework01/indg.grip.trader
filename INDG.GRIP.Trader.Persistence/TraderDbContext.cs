using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using INDG.GRIP.Trader.Domain.Common;
using INDG.GRIP.Trader.Domain.Aggregates.Products;

namespace INDG.GRIP.Trader.Persistence
{
    public class TraderDbContext : DbContext, IDbContext
    {
        public TraderDbContext(DbContextOptions<TraderDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TraderDbContext).Assembly);
        }

        private IDbContextTransaction _transaction;

        public async Task BeginTransactionAsync()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var dateTime = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                UpdateState(entry);
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = dateTime;
                        break;
                    case EntityState.Modified:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private static void UpdateState(EntityEntry<Entity> entry)
        {
            if (entry.Entity.IsNew)
            {
                entry.State = EntityState.Added;
            }
        }
    }
}