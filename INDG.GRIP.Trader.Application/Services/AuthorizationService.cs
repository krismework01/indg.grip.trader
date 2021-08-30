using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace INDG.GRIP.Trader.Application.Services
{
    public class AuthorizationService
    {
        public static SymmetricSecurityKey Create(string secret)
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}