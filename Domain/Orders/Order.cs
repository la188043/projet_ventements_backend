using System;
using Domain.Users;

namespace Domain.Orders
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderedAt { get; set; }
        public IUser Orderer { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Order order)
            {
                return Id == order.Id;
            }

            return false;
        }
    }
}