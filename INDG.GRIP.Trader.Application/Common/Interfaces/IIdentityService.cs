using System;
using INDG.GRIP.Trader.Application.Logic.Auth;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        IdentityMaster BuildIdentityMaster(User user);
        IdentityMaster BuildCookieIdentityMaster(User user);
        bool IsValidToken(DateTime smsCreated, int smsLifetime);
    }
}