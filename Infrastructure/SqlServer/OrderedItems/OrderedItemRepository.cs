using System.Collections.Generic;
using Application.Repositories;
using Domain.OrderedItems;

namespace Infrastructure.SqlServer.OrderedItems
{
    public class OrderedItemRepository : IOrderedItemRepository
    {
        public IEnumerable<IOrderedItem> GetByOrderId(int orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}