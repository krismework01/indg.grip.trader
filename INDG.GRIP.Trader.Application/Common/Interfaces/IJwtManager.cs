using System.Security.Claims;
using INDG.GRIP.Trader.Application.Logic.Auth;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface IJwtManager
    {
        SessionUser GenerationToken(ClaimsIdentity claim, User user);
    }
}