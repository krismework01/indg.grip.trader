using System;

namespace INDG.GRIP.Trader.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        Guid Id { get; init; }
        string Login { get; }
        string FirstName { get; }
        string LastName { get; }
        string SecurityToken { get; }
    }
}