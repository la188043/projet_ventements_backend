using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Items;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Shared;
using Infrastructure.SqlServer.SubCategories;
using ItemFactory = Infrastructure.SqlServer.Factories.ItemFactory;


namespace Infrastructure.SqlServer.Foods
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

                    if (reader.Read())
                    {
                        return _factory.CreateFromReader(reader);
                    }
                }

                return null;
            }
            //Post
            public IItem Create(int subcategoryId,IItem item)
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = ItemSqlServer.ReqAdd;

                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColLabel}", item.Label);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColPrice}", item.Price);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColQuantity}", item.Quantity);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColImageItem}", item.ImageItem);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColDescriptionItem}", item.DescriptionItem);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColSize}", item.Size);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColSubCategoryId}", item.SubcategoryId);

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
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColQuantity}", item.Quantity);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColImageItem}", item.ImageItem);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColDescriptionItem}", item.DescriptionItem);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColSize}", item.Size);
                    cmd.Parameters.AddWithValue($"@{ItemSqlServer.ColSubCategoryId}", item.SubcategoryId);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            
            
            public IEnumerable<IItem> GetBySubCategoryId(int subcategoryId)
            {
                IList<IItem> subcategories = new List<IItem>();
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = ItemSqlServer.ReqGetBySubCategoryId;
                    cmd.Parameters.AddWithValue($"{ItemSqlServer.ColSubCategoryId}", subcategoryId);

                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        subcategories.Add(_factory.CreateFromReader(reader));
                    }
                }
                return subcategories;
            }
        }

    }



    
