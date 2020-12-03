using Domain.Orders;
using Domain.Shared;
using Domain.Users;

namespace Domain.OrderedItems
{
    public interface IOrderedItem : IEntity
    {
        IOrder order { get; set; }
        IUser orderer { get; set; }
    }
}