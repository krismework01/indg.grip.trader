using INDG.GRIP.Trader.Domain.Aggregates.Products;
using INDG.GRIP.Trader.Domain.Aggregates.Users;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        Task<int> SaveChangeAsync(CancellationToken token);
    }
}