
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
            return new Category()
            {
                Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColId)),
                Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColTitle)),
                
            };
        }
    }
}