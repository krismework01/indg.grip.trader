using System;

namespace INDG.GRIP.Trader.Domain.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            :base(message)
        {
            
        }
    }
}