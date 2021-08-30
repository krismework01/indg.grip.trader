using System;

namespace INDG.GRIP.Trader.Domain.Common.Exceptions
{
    public class StatusException : Exception
    {
        public StatusException(string message)
            : base(message)
        {
        }

        public StatusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}