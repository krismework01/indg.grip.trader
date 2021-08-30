using System;
using System.Linq;

namespace INDG.GRIP.Trader.Application.Common.Models
{
    public record Result<T>
    {
        public Result(T value, string[] errors = null)
        {
            this.Value = value;
            this.Errors = errors ?? Array.Empty<string>();
        }

        public bool Success => !Errors.Any();
        public T Value { get; }

        public string[] Errors { get; }
    }
}