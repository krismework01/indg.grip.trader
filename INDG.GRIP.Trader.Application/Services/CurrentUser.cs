using INDG.GRIP.Trader.Application.Common.Interfaces;
using System;

namespace INDG.GRIP.Trader.Application.Services
{
    public class CurrentUser : ICurrentUser
    {
        public string Login { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public Guid Id { get; init; }
        public string SecurityToken { get; init; }
        
        public CurrentUser(string login, string firstName, string lastName, Guid id, string securityToken)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Id = id;
            SecurityToken = securityToken;
        }
    }
}