using System.Collections.Generic;
using Domain.OrderedItems;

namespace Application.Repositories
{
    public interface IOrderedItemRepository
    {
        IEnumerable<IOrderedItem> GetByOrderId(int orderId);
        IOrderedItem AddItemToOrder(int orderId, int itemId, IOrderedItem orderedItem);
    }
}