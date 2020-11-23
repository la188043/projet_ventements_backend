using Application.Services.Addresses.Dto;

namespace Application.Services.Addresses
{
    public interface IAddressService
    {
        OutputDtoQueryAddress GetById(int id);
        OutputDtoQueryAddress Create(InputDtoAddAddress address);
    }
}