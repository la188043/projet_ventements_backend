﻿using System.Data.SqlClient;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Users;
using Infrastructure.SqlServer.WishLists;

namespace Infrastructure.SqlServer.Factories
{
    public class WishListFactory : IInstanceFromReader<IWishlist>
    {
        public IWishlist CreateFromReader(SqlDataReader reader)
        {
            return new WishList
            {
                Id = reader.GetInt32(reader.GetOrdinal(WishListSqlServer.ColId)),
                AddedAt = reader.GetDateTime(reader.GetOrdinal(WishListSqlServer.ColDate)),
                UserWishList = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(WishListSqlServer.ColUserId)),
                    Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                    Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname))
                },
                ItemWishList = new Item
                {
                    Id = reader.GetInt32(reader.GetOrdinal(WishListSqlServer.ColItemId)),
                    Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel)),
                    Price = reader.GetFloat(reader.GetOrdinal(ItemSqlServer.ColPrice)),
                    ImageItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColImageItem)),
                    DescriptionItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColDescriptionItem))
                }
            };
        }
    }
}
