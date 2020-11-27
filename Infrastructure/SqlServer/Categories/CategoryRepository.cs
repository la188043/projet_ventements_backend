using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        public IEnumerable<ICategory> GetByCategoryId(int parentCategoryId)
        {
            IList<ICategory> subcategories = new List<ICategory>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = CategorySqlServer.ReqGetByCategoryId;

                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColCategoryId}", parentCategoryId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                    subcategories.Add(_factory.CreateFromReader(reader));
            }

            return subcategories;
        }

        public ICategory CreateCategory(ICategory category)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = CategorySqlServer.ReqCreateCategory;

                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColTitle}", category.Title);

                category.Id = (int) cmd.ExecuteScalar();
            }

            return category;
        }

        public ICategory CreateSubCategory(int parentCategoryId, ICategory childCategory)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = CategorySqlServer.ReqCreateSubCategory;

                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColTitle}", childCategory.Title);
                cmd.Parameters.AddWithValue($"@{CategorySqlServer.ColCategoryId}", parentCategoryId);

                childCategory.Id = (int) cmd.ExecuteScalar();
            }

            return childCategory;
        }
    }
}