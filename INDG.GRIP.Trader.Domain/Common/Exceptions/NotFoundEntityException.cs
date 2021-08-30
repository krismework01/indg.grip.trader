using System;

namespace INDG.GRIP.Trader.Domain.Common.Exceptions
{
    public class NotFoundEntityException : Exception
    {
        public NotFoundEntityException(string name, object key)
            : base($"Item \"{name}\" ({key}) not found.")
        {
        }

        public NotFoundEntityException(string message)
            : base(message)
        {
        }
    }
}