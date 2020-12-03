using System;
using Domain.Shared;
using Domain.Users;

namespace Domain.Orders
{
    public interface IOrder : IEntity
    {
        bool IsPaid { get; set; }
        DateTime orderedAt { get; set; }
        IUser Orderer { get; set; }
    }
}