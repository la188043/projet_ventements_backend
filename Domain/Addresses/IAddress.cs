using Domain.Shared;

namespace Domain.Addresses
{
    public interface IAddress : IEntity
    {
        string Street { get; set; }
        int HomeNumber { get; set; }
        string Zip { get; set; }
        string City { get; set; }
    }
}