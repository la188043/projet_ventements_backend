using System.Collections.Generic;
using Domain.OrderedItems;

namespace Application.Repositories
{
    public interface IOrderedItemRepository
    {
        IEnumerable<IOrderedItem> GetByOrderId(int orderId);
        IOrderedItem GetById(int orderedItemId);
        IOrderedItem AddItemToOrder(int orderId, int itemId, IOrderedItem orderedItem);
        bool UpdateQuantity(int orderedItemId, IOrderedItem orderedItem);
        bool Delete(int orderedItemId);
    }
}