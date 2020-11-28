using System.Data.SqlClient;
using Domain.Categories;
using Domain.Items;
using Infrastructure.SqlServer.Categories;
using Infrastructure.SqlServer.Items;

namespace Infrastructure.SqlServer.Factories
{
    public class ItemFactory : IInstanceFromReader<IItem>
    {
        public IItem CreateFromReader(SqlDataReader reader)
        {
            return new Item
            {
                Id = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColId)),
                Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel)),
                Price = (float) reader.GetDouble(reader.GetOrdinal(ItemSqlServer.ColPrice)),
                Quantity = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColQuantity)),
                ImageItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColImageItem)),
                DescriptionItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColDescriptionItem)),
                Size = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColSize)),
                
                Category = new Category
                {
                    Id = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColCategoryId)),
                    Title = reader.GetString(reader.GetOrdinal(CategorySqlServer.ColTitle))
                }
            };
        }
    }
}
