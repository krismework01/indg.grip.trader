namespace INDG.GRIP.Trader.Application.Common.Models
{
    public record AuthConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string Lifetime { get; set; }
    }
}