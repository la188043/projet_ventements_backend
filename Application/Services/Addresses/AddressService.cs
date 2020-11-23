using System.ComponentModel.DataAnnotations;
using Application.Repositories;
using Application.Services.Addresses.Dto;
using Domain.Addresses;

namespace Application.Services.Addresses
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _addressRepository;
        
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public OutputDtoQueryAddress GetById(int id)
        {
            var addressFromDb = _addressRepository.GetById(id);
            
            return new OutputDtoQueryAddress
            {
                Id = addressFromDb.Id,
                Street = addressFromDb.Street,
                HomeNumber = addressFromDb.HomeNumber,
                Zip = addressFromDb.Zip,
                City = addressFromDb.City
            };
        }

        public OutputDtoQueryAddress Create(InputDtoAddAddress address)
        {
            var addressFromDb = _addressRepository.Create(new Address
            {
                Street = address.Street,
                HomeNumber = address.HomeNumber,
                Zip = address.Zip,
                City = address.City
            });

            return new OutputDtoQueryAddress
            {
                Id = addressFromDb.Id,
                Street = addressFromDb.Street,
                HomeNumber = addressFromDb.HomeNumber,
                Zip = addressFromDb.Zip,
                City = addressFromDb.City
            };
        }
    }
}