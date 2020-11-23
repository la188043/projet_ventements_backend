using System.Collections.Generic;
 using System.Data;
 using Application.Repositories;
using Domain;
using Domain.Categories;
using Infrastructure.SqlServer.Factories;
 using Infrastructure.SqlServer.Shared;


 namespace Infrastructure.SqlServer.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IInstanceFromReader<ICategory> _factory = new CategoryFactory();
        public IEnumerable<ICategory> Query()
        {
            IList<ICategory> categories = new List<ICategory>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = CategorySqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    categories.Add(_factory.CreateFromReader(reader));
                }
            }
            return categories;
        }

        public ICategory GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = CategorySqlServer.ReqGetById;
                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    return _factory.CreateFromReader(reader);
                }
            }
            return null;
        }

        public ICategory Create(ICategory category)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = CategorySqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColTitle}", category.Title);

                category.Id = (int) cmd.ExecuteScalar();
            }
            return category;
        }

        public bool Update(int id, ICategory category)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = CategorySqlServer.ReqPut;

                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColId}", id);
                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColTitle}", category.Title);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}