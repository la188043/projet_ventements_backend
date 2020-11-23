using System.Data.SqlClient;
using Domain.Addresses;
using Infrastructure.SqlServer.Addresses;

namespace Infrastructure.SqlServer.Factories
{
    public class AddressFactory : IInstanceFromReader<IAddress>
    {
        public IAddress CreateFromReader(SqlDataReader reader)
        {
            return new Address
            {
                Id = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColId)),
                Street = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColStreet)),
                HomeNumber = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColHomeNumber)),
                Zip = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColZip)),
                City = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColCity))
            };
        }
    }
}