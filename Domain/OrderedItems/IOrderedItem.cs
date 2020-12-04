using Domain.Items;
using Domain.Orders;
using Domain.Shared;
using Domain.Users;

namespace Domain.OrderedItems
{
    public interface IOrderedItem : IEntity
    {
        IOrder Order { get; set; }
        IItem ItemOrdered { get; set; }
    }
}