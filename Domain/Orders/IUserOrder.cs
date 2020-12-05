using System;
using System.Collections.Generic;
using Domain.OrderedItems;
using Domain.Shared;
using Domain.Users;

namespace Domain.Orders
{
    public interface IUserOrder : IEntity
    {
        IUser Orderer { get; set; }
        bool IsPaid { get; set; }
        DateTime OrderedAt { get; set; }

        void AddOrderedItem(IOrderedItem orderedItem);
        void AddOrderedItems(IEnumerable<IOrderedItem> orderedItems);
        float ComputeOrderPrice();
    }
}