using System.Data.SqlClient;
using Domain.Categories;
using Domain.Items;
using Domain.SubCategories;
using Infrastructure.SqlServer.Categories;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.SubCategories;

namespace Infrastructure.SqlServer.Factories
{
    public class ItemFactory : IInstanceFromReader<IItem>
    {
        public IItem CreateFromReader(SqlDataReader reader)
        {
            return new Item()
            {
                Id = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColId)),
                Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel)),
                Price = (float) reader.GetDouble(reader.GetOrdinal(ItemSqlServer.ColPrice)),
                Quantity = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColQuantity)),
                ImageItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColImageItem)),
                DescriptionItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColDescriptionItem)),
                Size = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColSize)),
                
                SubCategory = new SubCategory
                {
                    Id = reader.GetInt32(reader.GetOrdinal(SubCategorySqlServer.ColId)),
                    Title = reader.GetString(reader.GetOrdinal(SubCategorySqlServer.ColTitle)),
                    
                    Category = new Category
                    {
                        Id = reader.GetInt32(reader.GetOrdinal(CategorySqlServer.ColId)),
                        Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColTitle))
                    }
                }
            };
        }
    }
}
