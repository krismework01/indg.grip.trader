using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Logic.Auth;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Services
{
    public class JwtManager : IJwtManager
    {
        private readonly AuthConfig _authConfig;

        public JwtManager(IOptions<AuthConfig> authConfig)
        {
            _authConfig = authConfig.Value;
        }

        public SessionUser GenerationToken(ClaimsIdentity identity, User user)
        {
            var now = DateTime.UtcNow;

            var lifetime = int.Parse(_authConfig.Lifetime);
            var expires = now.Add(TimeSpan.FromMinutes(lifetime));
            var signingCredentials = new SigningCredentials
            (
                key: AuthorizationService.Create(_authConfig.Key),
                algorithm: SecurityAlgorithms.HmacSha256
            );
            var jwt = new JwtSecurityToken
            (
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            jwt.Payload["user_id"] = user.Id;
            jwt.Payload["iat"] = now.TimeOfDay.Ticks;
            jwt.Payload["login"] = user.Login;
            jwt.Payload["first_name"] = user.FirstName;
            jwt.Payload["last_name"] = user.LastName;
            jwt.Payload["expires"] = expires;

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new SessionUser
            {
                Date = now,
                Expiration = lifetime,
                Token = encodedJwt,
                UserId = user.Id.ToString(),
                User = user.Login
            };
        }
    }
}