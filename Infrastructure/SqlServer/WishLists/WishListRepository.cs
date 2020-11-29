using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.WishLists
{
    public class WishListRepository : IWishListRepository
    {
        private readonly IInstanceFromReader<IWishlist> _factory = new WishListFactory();

        public IEnumerable<IWishlist> Query()
        {
            IList<IWishlist> wishlists = new List<IWishlist>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = WishListSqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    wishlists.Add(_factory.CreateFromReader(reader));
            }

            return wishlists;
        }

        public IEnumerable<IWishlist> GetByUserId(int uservId)
        {
            IList<IWishlist> wishlists = new List<IWishlist>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = WishListSqlServer.ReqGetByUserId;

                cmd.Parameters.AddWithValue($"@{WishListSqlServer.ColUserId}", uservId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    wishlists.Add(_factory.CreateFromReader(reader));
            }

            return wishlists;
        }

        public IWishlist Add(int uservId, int itemId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = WishListSqlServer.ReqCreate;
                cmd.Parameters.AddWithValue($"@{WishListSqlServer.ColItemId}", itemId);
                cmd.Parameters.AddWithValue($"@{WishListSqlServer.ColUserId}", uservId);

                return new WishList {Id = (int) cmd.ExecuteScalar()};
            }
        }

        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = WishListSqlServer.ReqDelete;

                cmd.Parameters.AddWithValue($"@{WishListSqlServer.ColId}", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}