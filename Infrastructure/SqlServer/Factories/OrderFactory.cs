using System.Data;
using System.Data.SqlClient;
using Domain.Orders;
using Domain.Users;
using Infrastructure.SqlServer.Orders;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factories
{
    public class OrderFactory : IInstanceFromReader<IOrder>
    {
        public IOrder CreateFromReader(SqlDataReader reader)
        {
            return new Order
            {
                Id = reader.GetInt32(reader.GetOrdinal(OrderSqlServer.ColId)),
                IsPaid = reader.GetBoolean(reader.GetOrdinal(OrderSqlServer.ColIsPaid)),
                orderedAt = reader.GetDateTime(reader.GetOrdinal(OrderSqlServer.ColOrderedAt)),
                Orderer = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(OrderSqlServer.ColUserId)),
                    Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                    Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname)),
                    Email = reader.GetString(reader.GetOrdinal(UserSqlServer.ColEmail))
                }
            };
        }
    }
}