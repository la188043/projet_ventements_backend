using System.Collections.Generic;
using Domain.OrderedItems;

namespace Application.Repositories
{
    public interface IOrderedItemRepository
    {
        IEnumerable<IOrderedItem> GetByOrderId(int orderId);
    }
}