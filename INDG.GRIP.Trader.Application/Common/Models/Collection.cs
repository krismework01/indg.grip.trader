using System.Collections.Generic;

namespace INDG.GRIP.Trader.Application.Common.Models
{
    public record Collection<T> where T : class, new ()
    {
        public Collection()
        {
            Data = new HashSet<T>();
        }
        public Collection(ICollection<T> data)
        {
            Data = data;
        }
        public Collection(ICollection<T> data, int count)
        {
            Data = data ?? new HashSet<T>();
            TotalCount = count;
        }
        public ICollection<T> Data { get; }
        public int TotalCount { get; }
    }
}