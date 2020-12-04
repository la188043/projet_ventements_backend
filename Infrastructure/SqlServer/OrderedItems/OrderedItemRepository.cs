using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.OrderedItems;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.OrderedItems
{
    public class OrderedItemRepository : IOrderedItemRepository
    {
        private IInstanceFromReader<IOrderedItem> _factory = new OrderedItemFactory();
        
        public IEnumerable<IOrderedItem> GetByOrderId(int orderId)
        {
            IList<IOrderedItem> orderedItems = new List<IOrderedItem>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderedItemSqlServer.ReqGetByOrderId;

                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColOrderId}", orderId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    orderedItems.Add(_factory.CreateFromReader(reader));
            }

            return orderedItems;
        }
    }
}