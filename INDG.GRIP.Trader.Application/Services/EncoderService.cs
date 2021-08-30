using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace INDG.GRIP.Trader.Application.Services
{
    public static class EncoderService
    {
        public static string GetSha256(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(login));

            var usUpper = login.ToUpper();
            var usLower = login.ToLower();

            using var sha256 = SHA256.Create();
            var part1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(usUpper));
            var part2 = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(usLower, ":", password)));

            var strPart1 = string.Join("", part1.Select(b => b.ToString("x2")).ToArray());
            var strPart2 = string.Join("", part2.Select(b => b.ToString("x2")).ToArray());

            var complete = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(strPart1, strPart2)));
            var strComplete = string.Join("", complete.Select(b => b.ToString("x2")).ToArray());

            return strComplete;
        }
    }
}
