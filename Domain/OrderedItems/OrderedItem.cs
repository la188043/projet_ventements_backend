﻿using Domain.Items;
using Domain.Orders;
using Domain.Users;

namespace Domain.OrderedItems
{
    public class OrderedItem : IOrderedItem
    {
        public int Id { get; set; }
        public IOrder Order { get; set; }
        public IItem ItemOrdered { get; set; }

        public OrderedItem()
        {
        }
    }
}