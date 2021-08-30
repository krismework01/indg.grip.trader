using INDG.GRIP.Trader.Domain.Aggregates.Users;
using INDG.GRIP.Trader.Domain.Common;
using System;

namespace INDG.GRIP.Trader.Domain.Aggregates.Products
{
    public class Product : IdentityEntity<Guid>
    {
        private Product()
        {
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        
        private int _statusId;
        public Status Status { get; private set; }
        public Guid SalerUserId { get; private set; }
        public User Saler { get; private set; }
        public Guid? BuyerUserId { get; private set; }
        public User Buyer { get; private set; }
        public string ShippingNumber { get; private set; }

        public Product(string name, string description, int price, Guid salerUserId, Status status)
            : this()
        {
            Name = name;
            Description = description;
            Price = price;
            SalerUserId = salerUserId;
            SetStatus(status);
        }

        private void SetStatus(Status status)
        {
            _statusId = status.Id;
        }

        public void SetSaled(Guid byerUserId)
        {
            var status = Status.From(Status.Saled.Id);
            BuyerUserId = byerUserId;
            SetStatus(status);
        }

        public void SetShipped(string shippingNumber)
        {
            var status = Status.From(Status.Shipped.Id);
            ShippingNumber = shippingNumber;
            SetStatus(status);
        }
    }
}
