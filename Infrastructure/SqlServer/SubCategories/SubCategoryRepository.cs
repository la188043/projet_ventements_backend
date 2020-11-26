﻿using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.SubCategories;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.SubCategories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IInstanceFromReader<ISubCategory> _factory = new SubCategoryFactory();

        // Query
        /*
        public IEnumerable<ISubCategory> Query()
        {
            IList<ISubCategory> categories = new List<ISubCategory>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = SubCategorySqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    categories.Add(_factory.CreateFromReader(reader));
                }
            }

            return categories;
        }
        */

        // Get
        public ISubCategory GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = SubCategorySqlServer.ReqGetById;
                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    return _factory.CreateFromReader(reader);
                }
            }

            return null;
        }

        public IEnumerable<ISubCategory> GetByCategoryId(int categoryId)
        {
            IList<ISubCategory> categories = new List<ISubCategory>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = SubCategorySqlServer.ReqGetByCategoryId;
                cmd.Parameters.AddWithValue($"{SubCategorySqlServer.ColIdCategory}", categoryId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    categories.Add(_factory.CreateFromReader(reader));
                }
            }

            return categories;
        }

        // Post
        public ISubCategory Create(int categoryId, ISubCategory subCategory)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = SubCategorySqlServer.ReqAdd;

                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColTitle}", subCategory.Title);
                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColIdCategory}", categoryId);

                subCategory.Id = (int) cmd.ExecuteScalar();
            }

            return subCategory;
        }

        // Update
        /*
        public bool Update(int id, ISubCategory subCategory)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = SubCategorySqlServer.ReqPut;

                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColId}", id);
                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColTitle}", subCategory.Title);
                cmd.Parameters.AddWithValue($"@{SubCategorySqlServer.ColIdCategory}", subCategory.CategoryId);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        */
    }
}