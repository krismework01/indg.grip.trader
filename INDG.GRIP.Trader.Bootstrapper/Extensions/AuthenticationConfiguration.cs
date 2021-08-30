using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using INDG.GRIP.Trader.Application.Common.Models;
using INDG.GRIP.Trader.Application.Services;

namespace INDG.GRIP.Trader.Bootstrapper.Extensions
{
    internal static class AuthenticationConfiguration
    {
        internal static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var auth = configuration.GetSection("Auth").Get<AuthConfig>();
            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = auth.Issuer,
                            ValidAudience = auth.Audience,
                            IssuerSigningKey = AuthorizationService.Create(auth.Key)
                        };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            string accessToken;
                            KeyValuePair<string, StringValues>? header;
            
                            header = context.Request.Headers.FirstOrDefault(x => x.Key == "Authorization");
                            accessToken = header.Value.Value;
                            if (string.IsNullOrEmpty(accessToken))
                            {
                                var token = context.Request.Query["access_token"];
                                if (!string.IsNullOrEmpty(token))
                                    accessToken = token;
                            }
            
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/notify/negotiate")
                                 || path.StartsWithSegments("/notify")))
                                context.Token = accessToken.Replace("Bearer ", string.Empty);
            
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}