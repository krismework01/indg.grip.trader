using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Domain.Aggregates.Products;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Application.Logic.System
{
    public struct SeedCommand : IRequest
    {
    }

    public class SeedCommandHandler : IRequestHandler<SeedCommand>
    {
        private readonly IDbContext _context;

        public SeedCommandHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
        {
            await SeedStatus(_context, cancellationToken);

            return Unit.Value;
        }

        private static async Task SeedStatus(IDbContext context, CancellationToken token)
        {
            if (context.Status.Any())
                return;

            var statuses = new List<Status>
            {
                Status.OnSale,
                Status.Saled,
                Status.Shipped               
            };

            await context.Status.AddRangeAsync(statuses, token);
            await context.SaveChangesAsync(token);
        }
    }
}
