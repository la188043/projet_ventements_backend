using Application.Repositories;
using Domain.Addresses;

namespace Infrastructure.SqlServer.Addresses
{
    public class AddressRepository : IAddressRepository
    {
        public IAddress GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IAddress Create(IAddress address)
        {
            throw new System.NotImplementedException();
        }
    }
}