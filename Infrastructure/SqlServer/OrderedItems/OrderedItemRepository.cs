using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.Exceptions;
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

        public IOrderedItem GetById(int orderedItemId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderedItemSqlServer.ReqGetById;

                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColId}", orderedItemId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
        }

        public IOrderedItem AddItemToOrder(int orderId, int itemId, IOrderedItem orderedItem)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderedItemSqlServer.ReqAddItemToOrder;

                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColQuantity}", orderedItem.Quantity);
                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColSize}", orderedItem.Size);
                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColOrderId}", orderId);
                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColItemId}", itemId);

                try
                {
                    orderedItem.Id = (int) cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                    throw new DuplicateSqlPrimaryException("Cet objet est déjà présent dans la commande");
                }
            }

            return orderedItem;
        }

        public bool UpdateQuantity(int orderedItemId, IOrderedItem orderedItem)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderedItemSqlServer.ReqUpdateQuantity;

                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColQuantity}", orderedItem.Quantity);
                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColId}", orderedItemId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int orderedItemId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderedItemSqlServer.ReqDelete;

                cmd.Parameters.AddWithValue($"@{OrderedItemSqlServer.ColId}", orderedItemId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}