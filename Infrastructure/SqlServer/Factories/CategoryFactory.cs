
using System.Data.SqlClient;
using Domain;
using Domain.Categories;
using Infrastructure.SqlServer.Categories;

namespace Infrastructure.SqlServer.Factories
{
    public class CategoryFactory: IInstanceFromReader<ICategory>
    {
        public ICategory CreateFromReader(SqlDataReader reader)
        {
            Category parentCategory  = null;
            if (!reader.IsDBNull(reader.GetOrdinal(CategorySqlServer.ColAliasParentId)))
            {
                parentCategory = new Category
                {
                    Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColAliasParentId)),
                    Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColAliasParentTitle))
                };
            }
            
            return new Category()
            {
                Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColAliasChildId)),
                Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColAliasChildTitle)),
                ParentCategory = parentCategory
            };
        }
    }
}