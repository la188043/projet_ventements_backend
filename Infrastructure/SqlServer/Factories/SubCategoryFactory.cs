using System.Data.SqlClient;
using Domain;
using Domain.SubCategories;
using Infrastructure.SqlServer.Categories;
using Infrastructure.SqlServer.SubCategories;

namespace Infrastructure.SqlServer.Factories
{
    public class SubCategoryFactory : IInstanceFromReader<ISubCategory>
    {
        public ISubCategory CreateFromReader(SqlDataReader reader)
        {
            return new SubCategory
            {
                Id = reader.GetInt32(reader.GetOrdinal(SubCategorySqlServer.ColId)),
                Title = reader.GetString(reader.GetOrdinal(SubCategorySqlServer.ColTitle)),
                CategoryId = reader.GetInt32(reader.GetOrdinal(SubCategorySqlServer.ColIdCategory))

            };
        }


    }
}