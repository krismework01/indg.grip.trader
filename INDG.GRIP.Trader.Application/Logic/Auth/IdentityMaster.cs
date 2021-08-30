using System.Security.Claims;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Logic.Auth
{
    public class IdentityMaster
    {
        public ClaimsIdentity Identity { get; set; }
        public User user { get; set; }
    }
}