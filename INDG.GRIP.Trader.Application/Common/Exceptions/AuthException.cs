using System;

namespace INDG.GRIP.Trader.Application.Common.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message)
            : base(message)
        {
        }
    }
}