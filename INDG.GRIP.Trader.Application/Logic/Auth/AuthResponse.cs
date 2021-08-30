using System;

namespace INDG.GRIP.Trader.Application.Logic.Auth
{
    public record AuthResponse
    {
        public string Token { get; init; }
    }
}