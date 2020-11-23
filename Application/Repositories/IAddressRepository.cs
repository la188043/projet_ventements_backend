using Domain.Addresses;

namespace Application.Repositories
{
    public interface IAddressRepository
    {
        IAddress GetById(int id);
        IAddress Create(IAddress address);
    }
}