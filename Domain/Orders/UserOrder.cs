using System;
using System.Collections.Generic;
using System.Linq;
using Domain.OrderedItems;
using Domain.Users;

namespace Domain.Orders
{
    public class UserOrder : IUserOrder
    {
        public int Id { get; set; }
        public IUser Orderer { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderedAt { get; set; }
        public IList<IOrderedItem> OrderedItems { get; set; }

        public UserOrder()
        {
            OrderedItems = new List<IOrderedItem>();
        }

        public void AddOrderedItem(IOrderedItem orderedItem)
        {
            OrderedItems.Add(orderedItem);
        }

        public void AddOrderedItems(IEnumerable<IOrderedItem> orderedItems)
        {
            foreach (var orderedItem in orderedItems)
            {
                OrderedItems.Add(orderedItem);
            }
        }

        public float ComputeOrderPrice()
        {
            return OrderedItems.Select(orderedItem => orderedItem.ItemOrdered.Price).Sum();
        }
    }
}