using Domain.Orders;
using Domain.Users;

namespace Domain.OrderedItems
{
    public class OrderedItem : IOrderedItem
    {
        public int Id { get; set; }
        public IOrder order { get; set; }
        public IUser orderer { get; set; }

        public OrderedItem()
        {
        }
    }
}