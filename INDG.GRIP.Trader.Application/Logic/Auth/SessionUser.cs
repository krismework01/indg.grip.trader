using System;

namespace INDG.GRIP.Trader.Application.Logic.Auth
{
    public record SessionUser
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Token { get; set; }
        public int Expiration { get; set; }
    }
}