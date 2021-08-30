using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace INDG.GRIP.Trader.Domain.Aggregates.Users
{
    public interface IUserRepository
    {
        Task<Guid> AddUser(User user, CancellationToken token = default);
        Task<User> GetUserByCondition(Expression<Func<User, bool>> predicate, CancellationToken token = default);
    }
}