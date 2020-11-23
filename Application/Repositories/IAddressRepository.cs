using Domain.Addresses;

namespace Application.Repositories
{
    public interface IAddressRepository
    {
        IAddress GetById(int id);
        IAddress CheckFromDb(IAddress address);
        IAddress Create(IAddress address);
    }
}