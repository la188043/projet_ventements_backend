using Domain.Items;
using Domain.Orders;
using Domain.Shared;
using Domain.Users;

namespace Domain.OrderedItems
{
    public interface IOrderedItem : IEntity
    {
        int Quantity { get; set; }
        string Size { get; set; }
        IOrder Order { get; set; }
        IItem ItemOrdered { get; set; }
    }
}