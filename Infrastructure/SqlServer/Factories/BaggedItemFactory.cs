using System.Data.SqlClient;
using Domain.BaggedItems;
using Domain.Items;
using Domain.Users;
using Infrastructure.SqlServer.BaggedItems;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factories
{
    public class BaggedItemFactory : IInstanceFromReader<IBaggedItem>
    {
        public IBaggedItem CreateFromReader(SqlDataReader reader)
        {
            return new BaggedItem
            {
                Id = reader.GetInt32(reader.GetOrdinal(BaggedItemSqlServer.ColId)),
                AddedAt = reader.GetDateTime(reader.GetOrdinal(BaggedItemSqlServer.ColAddedAt)),
                Quantity = reader.GetInt32(reader.GetOrdinal(BaggedItemSqlServer.ColQuantity)),
                Size = reader.GetString(reader.GetOrdinal(BaggedItemSqlServer.ColSize)),
                BagOwner = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(BaggedItemSqlServer.ColUserId)),
                    Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                    Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname))
                },
                AddedItem = new Item
                {
                    Id = reader.GetInt32(reader.GetOrdinal(BaggedItemSqlServer.ColItemId)),
                    Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel)),
                    Price = (float) reader.GetDouble(reader.GetOrdinal(ItemSqlServer.ColPrice)),
                    ImageItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColImageItem)),
                    DescriptionItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColDescriptionItem))
                }
            };
        }
    }
}