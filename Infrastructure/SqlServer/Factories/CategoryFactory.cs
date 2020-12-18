using System.Data.SqlClient;
using Domain.Categories;
using Infrastructure.SqlServer.Categories;

namespace Infrastructure.SqlServer.Factories
{
    public class CategoryFactory : IInstanceFromReader<ICategory>
    {
        public ICategory CreateFromReader(SqlDataReader reader)
        {
            Category parentCategory = null;
            try
            {
                parentCategory = new Category
                {
                    Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColAliasParentId)),
                    Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColAliasParentTitle))
                };
            }
            catch
            {
            }

            return new Category
            {
                Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColAliasChildId)),
                Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColAliasChildTitle)),
                ParentCategory = parentCategory
            };
        }
    }
}