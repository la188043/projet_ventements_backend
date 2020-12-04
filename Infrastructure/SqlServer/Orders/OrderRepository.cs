using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Orders;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private IInstanceFromReader<IOrder> _factory = new OrderFactory();
        
        public IEnumerable<IOrder> GetByUserId(int userId)
        {
            IList<IOrder> orders = new List<IOrder>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderSqlServer.ReqGetByUserId;

                cmd.Parameters.AddWithValue($"@{OrderSqlServer.ColUserId}", userId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    orders.Add(_factory.CreateFromReader(reader));
            }

            return orders;
        }

        public IOrder GetById(int orderId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = OrderSqlServer.ReqGetById;

                cmd.Parameters.AddWithValue($"@{OrderSqlServer.ColId}", orderId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
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