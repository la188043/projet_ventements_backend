using Domain.Items;
using Domain.Orders;

namespace Domain.OrderedItems
{
    public class OrderedItem : IOrderedItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public IOrder Order { get; set; }
        public IItem ItemOrdered { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is OrderedItem item)
            {
                return ItemOrdered.Equals(item.ItemOrdered) && Order.Equals(item.Order);
            }

            return false;
        }
    }
}