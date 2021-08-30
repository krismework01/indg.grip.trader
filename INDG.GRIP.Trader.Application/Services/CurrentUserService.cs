using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;
using INDG.GRIP.Trader.Application.Common.Interfaces;
using System;

namespace INDG.GRIP.Trader.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public ICurrentUser CreateUserByToken(string jwt)
        {
            var jwtData = ReadJwtTokenExt(jwt);

            return new CurrentUser(jwtData.login, jwtData.firstName,
                jwtData.lastName, jwtData.userId,
                jwt);
        }

        public virtual ICurrentUser GetCurrentUser()
        {
            var jwt = GetJWTFromHttpContext();
            var jwtData = ReadJwtTokenExt(jwt);

            return new CurrentUser(jwtData.login, jwtData.firstName,
                jwtData.lastName, jwtData.userId,
                jwt);
        }

        private string GetJWTFromHttpContext()
        {
            var jwt = (string)_contextAccessor.HttpContext?.Request?.Headers
                          ?.FirstOrDefault(x => x.Key == "Authorization").Value ?? string.Empty;

            if (string.IsNullOrEmpty(jwt.ToString()))
                jwt = _contextAccessor.HttpContext?.Request.Query["access_token"];

            if (jwt is null)
                jwt = null;
            else
                jwt = jwt
                    .Replace("Bearer ", string.Empty)
                    .Replace("bearer ", string.Empty);

            return jwt;
        }
       

        private (string firstName, string lastName) ReadJwtToken(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
                return (string.Empty, string.Empty);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var firstName = token.Claims.FirstOrDefault(x => x.Type.Equals("first_name"))?.Value;
            var lastName = token.Claims.FirstOrDefault(x => x.Type.Equals("last_name"))?.Value;

            return (firstName, lastName);
        }

        private (string login, Guid userId, string firstName, string lastName) ReadJwtTokenExt(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
                return (string.Empty, Guid.Empty, string.Empty, string.Empty);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var firstName = token.Claims.FirstOrDefault(x => x.Type.Equals("first_name"))?.Value;
            var lastName = token.Claims.FirstOrDefault(x => x.Type.Equals("last_name"))?.Value;
            var login = token.Claims.FirstOrDefault(x => x.Type.Equals("login"))?.Value;
            var userIdString = token.Claims.SingleOrDefault(x => x.Type.Equals("user_id"))?.Value;

            Guid.TryParse(userIdString, out var userId);

            return (login, userId, firstName, lastName);
        }
    }
}