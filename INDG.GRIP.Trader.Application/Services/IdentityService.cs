using System;
using System.Collections.Generic;
using System.Security.Claims;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using INDG.GRIP.Trader.Application.Logic.Auth;
using INDG.GRIP.Trader.Domain.Aggregates.Users;

namespace INDG.GRIP.Trader.Application.Services
{
    public class IdentityService : IIdentityService
    {
        public IdentityMaster BuildIdentityMaster(User user)
        {
            List<Claim> claims;
            ClaimsIdentity claimsIdentity;
            IdentityMaster identity;
            try
            {
                claims = BuildClaims(user);

                claimsIdentity = BuildClaimsIdentity("Token", claims);

                identity = new IdentityMaster
                {
                    Identity = claimsIdentity,
                    user = user
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString());
            }

            return identity;
        }

        public IdentityMaster BuildCookieIdentityMaster(User user)
        {
            List<Claim> claims;
            ClaimsIdentity claimsIdentity;
            IdentityMaster identity;
            try
            {
                claims = BuildClaims(user);

                claimsIdentity = BuildClaimsIdentity("ApplicationCookie", claims);
                identity = new IdentityMaster
                {
                    Identity = claimsIdentity,
                    user = user
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.ToString());
            }

            return identity;
        }


        public bool IsValidToken(DateTime smsCreated, int smsLifetime)
            => DateTime.UtcNow <= smsCreated.AddMinutes(smsLifetime);
        
        private static List<Claim> BuildClaims(User user)
        {
            List<Claim> claims;
            claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String),
                new (ClaimsIdentity.DefaultNameClaimType, user.Login, ClaimValueTypes.String)
            };
            return claims;
        }

        private static ClaimsIdentity BuildClaimsIdentity(string authenticationType, IEnumerable<Claim> claims)
        {
            return new ClaimsIdentity(
                claims,
                authenticationType,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}