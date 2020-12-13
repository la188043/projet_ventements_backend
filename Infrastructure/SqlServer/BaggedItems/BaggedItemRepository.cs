using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.Repositories;
using Domain.BaggedItems;
using Domain.Exceptions;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.BaggedItems
{
    public class BaggedItemRepository : IBaggedItemRepository
    {
        private readonly IInstanceFromReader<IBaggedItem> _factory = new BaggedItemFactory();
        
        public IEnumerable<IBaggedItem> GetByUserId(int userId)
        {
            IList<IBaggedItem> items = new List<IBaggedItem>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = BaggedItemSqlServer.ReqQueryJoinUsersAndItems;

                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColUserId}", userId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    items.Add(_factory.CreateFromReader(reader));
            }

            return items;
        }

        public IBaggedItem AddToBag(int userId, int itemId, IBaggedItem baggedItem)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = BaggedItemSqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColUserId}", userId);
                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColItemId}", itemId);
                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColQuantity}", baggedItem.Quantity);
                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColSize}", baggedItem.Size);

                try
                {
                    baggedItem.Id = (int) cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                    throw new DuplicateException("Cet article est déjà présent dans le panier");
                }
            }

            return baggedItem;
        }

        public int EmptyBag(int userId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = BaggedItemSqlServer.ReqEmptyBag;

                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColUserId}", userId);

                return cmd.ExecuteNonQuery();
            }
        }

        public bool DeleteItem(int baggedItemId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = BaggedItemSqlServer.ReqDeleteItem;

                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColId}", baggedItemId);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateQuantity(int baggedItemId, IBaggedItem baggedItem)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = BaggedItemSqlServer.ReqUpdateQuantity;

                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColId}", baggedItemId);
                cmd.Parameters.AddWithValue($"@{BaggedItemSqlServer.ColQuantity}", baggedItem.Quantity);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}