using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Items;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Items
{
    public class ItemRepository : IItemRepository
    {
        private readonly IInstanceFromReader<IItem> _factory = new ItemFactory();

        //Query
        public IEnumerable<IItem> Query()
        {
            IList<IItem> items = new List<IItem>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ItemSqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    items.Add(_factory.CreateFromReader(reader));
                }
            }

            return items;
        }

        //Get
        public IItem GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = ItemSqlServer.ReqGetById;
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    return _factory.CreateFromReader(reader);
                }
            }

            return null;
        }

        public IEnumerable<IItem> GetByCategoryId(int categoryId)
        {
            IList<IItem> subcategories = new List<IItem>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ItemSqlServer.ReqGetByCategoryId;
                cmd.Parameters.AddWithValue($"{ItemSqlServer.ColCategoryId}", categoryId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    subcategories.Add(_factory.CreateFromReader(reader));
                }
            }

            return subcategories;
        }
        
        //Post
        public IItem Create(int categoryId, IItem item)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ItemSqlServer.ReqAdd;

                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColLabel}", item.Label);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColPrice}", item.Price);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColImageItem}", item.ImageItem);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColDescriptionItem}", item.DescriptionItem);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColCategoryId}", categoryId);

                item.Id = (int) cmd.ExecuteScalar();
            }

            return item;
        }

        //Put
        public bool Update(int id, IItem item)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = ItemSqlServer.ReqPut;

                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColId}", id);
                
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColLabel}", item.Label);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColPrice}", item.Price);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColImageItem}", item.ImageItem);
                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColDescriptionItem}", item.DescriptionItem);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ItemSqlServer.ReqDelete;

                cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColId}", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}