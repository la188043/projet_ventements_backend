using System.Collections.Generic;
using Application.Repositories;
using Domain.Orders;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<IOrder> GetByUserId(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IOrder Create(int userId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderSqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{OrderSqlServer.ColUserId}", userId);

                return new Order {Id = (int) cmd.ExecuteScalar()};
            }
        }
    }
}