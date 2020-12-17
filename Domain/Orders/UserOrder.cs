using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
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
        public IList<IOrderedItem> OrderedItems { get; }

        public UserOrder()
        {
            OrderedItems = new List<IOrderedItem>();
        }

        public void AddOrderedItem(IOrderedItem orderedItem)
        {
            if (OrderedItems.Contains(orderedItem)) throw new DuplicateItemException();
            
            OrderedItems.Add(orderedItem);
        }

        public void AddOrderedItems(IEnumerable<IOrderedItem> orderedItems)
        {
            foreach (var orderedItem in orderedItems)
            {
                try
                {
                    AddOrderedItem(orderedItem);
                }
                catch (DuplicateItemException) 
                {}
            }
        }

        public float ComputeOrderPrice()
        {
            return OrderedItems.Select(orderedItem => orderedItem.ItemOrdered.Price * orderedItem.Quantity).Sum();
        }
    }
}