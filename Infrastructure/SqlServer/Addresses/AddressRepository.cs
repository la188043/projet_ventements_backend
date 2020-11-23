using System.Data;
using Application.Repositories;
using Domain.Addresses;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Addresses
{
    public class AddressRepository : IAddressRepository
    {
        private IInstanceFromReader<IAddress> _factory = new AddressFactory();
        public IAddress GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = AddressSqlServer.ReqGetById;

                cmd.Parameters.AddWithValue($"@{AddressSqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
        }

        public IAddress Create(IAddress address)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = AddressSqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{AddressSqlServer.ColStreet}", address.Street);
                cmd.Parameters.AddWithValue($"@{AddressSqlServer.ColHomeNumber}", address.HomeNumber);
                cmd.Parameters.AddWithValue($"@{AddressSqlServer.ColZip}", address.Zip);
                cmd.Parameters.AddWithValue($"@{AddressSqlServer.ColCity}", address.City);

                address.Id = (int) cmd.ExecuteScalar();
            }

            return address;
        }
    }
}