using System;
using Domain.Users;

namespace Domain.Orders
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime orderedAt { get; set; }
        public IUser Orderer { get; set; }

        public Order()
        {
        }
    }
}