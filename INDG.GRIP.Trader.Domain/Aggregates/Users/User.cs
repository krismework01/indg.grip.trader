using System;
using INDG.GRIP.Trader.Domain.Common;

namespace INDG.GRIP.Trader.Domain.Aggregates.Users
{
    public class User : IdentityEntity<Guid>
    {
        private User()
        {
            Id = Guid.NewGuid();
        }

        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }

        public User(string login, string password, string firstName, string lastName)
            :this()
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}