using System;
using Application.Repositories;
using Application.Services.Addresses;
using Application.Services.Addresses.Dto;
using Domain.Addresses;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace UnitTests.Application
{
    [TestFixture]
    public class AddressServiceTest
    {
        public static IAddress CreateAddress(int i)
        {
            return new Address
            {
                Id = i,
                Street = i.ToString(),
                HomeNumber = i,
                Zip = i.ToString(),
                City = i.ToString()
            };
        }

        public static IAddress CreateAddressWithoutId(int i)
        {
            return new Address
            {
                Street = i.ToString(),
                HomeNumber = i,
                Zip = i.ToString(),
                City = i.ToString()
            };
        }

        public static InputDtoAddAddress CreateInputDtoAddAddress(int i)
        {
            return new InputDtoAddAddress
            {
                Street = i.ToString(),
                HomeNumber = i,
                Zip = i.ToString(),
                City = i.ToString()
            };
        }

        public static OutputDtoQueryAddress CreateOutputDtoQueryAddress(int i)
        {
            return new OutputDtoQueryAddress
            {
                Id = i,
                Street = i.ToString(),
                HomeNumber = i,
                Zip = i.ToString(),
                City = i.ToString()
            };
        }

        [Test]
        public void GetById_SingleAddress_ReturnsOutputAddressDto()
        {
            // ARRANGE //
            // Configure IAddressRepository Substitute
            var addressRep = Substitute.For<IAddressRepository>();
            addressRep.GetById(1).Returns(CreateAddress(1));

            // Inject it
            var addressService = new AddressService(addressRep);

            // 
            var expected = CreateOutputDtoQueryAddress(1);

            // ACT //
            var outputAddress = addressService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, outputAddress);
        }

        [Test]
        public void CheckFromDb_SingleAddress_ReturnsSameAddressFound()
        {
            // ARRANGE //
            var addressRep = Substitute.For<IAddressRepository>();
            addressRep.CheckFromDb(Arg.Any<IAddress>()).Returns(CreateAddress(1));

            // Address service
            var addressService = new AddressService(addressRep);

            // Expectation
            var expected = CreateOutputDtoQueryAddress(1);

            // ACT //
            var inputAddress = CreateInputDtoAddAddress(1);
            var outputAddress = addressService.CheckFromDb(inputAddress);

            // ASSERT //
            Assert.AreEqual(expected, outputAddress);
        }

        [Test]
        public void CheckFromDb_SingleAddress_ReturnsNullBecauseNotFound()
        {
            // ARRANGE //
            var addressRep = Substitute.For<IAddressRepository>();
            addressRep.CheckFromDb(Arg.Any<IAddress>()).ReturnsNull();
            
            // Address Service
            var addressService = new AddressService(addressRep);
            
            // ACT //
            var outputAddress = addressService.CheckFromDb(CreateInputDtoAddAddress(1));

            // ASSERT //
            Assert.AreEqual(null, outputAddress);
        }
    }
}