using INDG.GRIP.Trader.Domain.Common;
using INDG.GRIP.Trader.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace INDG.GRIP.Trader.Domain.Aggregates.Products
{
    public class Status : Enumeration
    {
        protected Status(int id, string name)
            : base(id, name)
        {
        }

        public static Status OnSale = new Status(1, "On sale");
        public static Status Saled = new Status(2, "Saled");
        public static Status Shipped = new Status(3, "Shipped");

        private static IEnumerable<Status> List() => new[] { OnSale, Saled, Shipped };

        public static Status FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new StatusException($"Possible values for {nameof(Status)}: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Status From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new StatusException($"Possible values for {nameof(Status)}: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
