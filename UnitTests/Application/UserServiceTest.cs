using System;
using System.Collections.Generic;
using Application;
using Application.Exceptions;
using Application.Repositories;
using Application.Services.Addresses;
using Application.Services.Addresses.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Addresses;
using Domain.Users;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class UserServiceTest
    {
        public static IUser CreateUser(int i)
        {
            return new User
            {
                Id = i,
                Firstname = i.ToString(),
                Lastname = i.ToString(),
                Email = $"{i}@gmail.com",
                Birthdate = DateTime.Now,
                EncryptedPassword = i.ToString(),
                Administrator = false,
                Gender = 'M',
                Address = new Address
                {
                    Id = i,
                    Street = i.ToString(),
                    HomeNumber = i,
                    Zip = i.ToString(),
                    City = i.ToString()
                }
            };
        }

        public static IEnumerable<IUser> CreateListOfUsers(int sizeOfList)
        {
            IList<IUser> users = new List<IUser>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                users.Add(CreateUser(i));
            }

            return users;
        }

        public static OutputDtoQueryUser CreateOutputDtoQueryUser(int i)
        {
            return new OutputDtoQueryUser
            {
                Id = i,
                Firstname = i.ToString(),
                Lastname = i.ToString(),
                Email = $"{i}@gmail.com",
                Birthdate = DateTime.Now,
                Administrator = false,
                Gender = 'M',
                UserAddress = new OutputDtoQueryUser.Address
                {
                    Id = i,
                    Street = i.ToString(),
                    HomeNumber = i,
                    Zip = i.ToString(),
                    City = i.ToString()
                }
            };
        }

        public static IEnumerable<OutputDtoQueryUser> CreateListOfOutputDtoQueryUsers(int sizeOfList)
        {
            IList<OutputDtoQueryUser> users = new List<OutputDtoQueryUser>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                users.Add(CreateOutputDtoQueryUser(i));
            }

            return users;
        }

        public static OutputDtoAuthenticateUser CreateOutputDtoAuthenticateUser(int i)
        {
            return new OutputDtoAuthenticateUser(CreateUser(i), $"{i}.{i}.{i}");
        }

        public static InputDtoAddUser CreateInputDtoAddUser(int i)
        {
            return new InputDtoAddUser
            {
                Firstname = i.ToString(),
                Lastname = i.ToString(),
                Email = $"{i}@gmail.com",
                PasswordUser = i.ToString(),
                Birthdate = DateTime.Now,
                Gender = "M"
            };
        }

        public static InputDtoAuthenticateUser CreateInputDtoAuthenticateUser(int i)
        {
            return new InputDtoAuthenticateUser
            {
                Email = $"{i}@gmail.com",
                PasswordUser = i.ToString()
            };
        }
        
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
        [TestCase(0)]
        [TestCase(4)]
        [TestCase(9)]
        [TestCase(24)]
        public void Query_ReturnsListOfOutputDtoQueryUser(int nbOfUsers)
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            userRep.Query().Returns(CreateListOfUsers(nbOfUsers));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateListOfOutputDtoQueryUsers(nbOfUsers);

            // ACT //
            var output = userService.Query();

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void GetById_SingleNumber_ReturnsOutputDtoQueryUser()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            userRep.GetById(1).Returns(CreateUser(1));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoQueryUser(1);

            // ACT //
            var output = userService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Create_InputDtoAddUser_ReturnsOutputDtoAddUser()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            passwordEncryption.HashPassword(Arg.Any<IUser>(), Arg.Any<string>())
                .Returns("hashed");

            userRep.Create(Arg.Any<IUser>()).Returns(CreateUser(1));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoAuthenticateUser(1);

            // ACT //
            var output = userService.Create(CreateInputDtoAddUser(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Create_InputDtoAddUser_ThrowsException()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            passwordEncryption.HashPassword(Arg.Any<IUser>(), Arg.Any<string>())
                .Returns("hashed");

            userRep.Create(Arg.Any<IUser>())
                .Returns(x => throw new DuplicateSqlPrimaryException("message"));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            
            // ASSERT //
            Assert.Throws<DuplicateSqlPrimaryException>(() => userService.Create(CreateInputDtoAddUser(1)));
        }

        [Test]
        public void Authenticate_InputDtoAuthenticateUser_ReturnsOutputDtoAuthenticateUser()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            passwordEncryption
                .VerifyPassword(Arg.Any<IUser>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            userRep.Authenticate(Arg.Any<IUser>()).Returns(CreateUser(1));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoAuthenticateUser(1);

            // ACT //
            var output = userService.Authenticate(CreateInputDtoAuthenticateUser(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Authenticate_InputDtoAuthenticateUser_ThrowsNullUserException()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            userRep.Authenticate(Arg.Any<IUser>()).ReturnsNull();

            var userService = new UserService(userRep, addressService, passwordEncryption);
            
            // ASSERT //
            Assert.Throws<NullUserException>(
                () => userService.Authenticate(CreateInputDtoAuthenticateUser(1)));
        }
        
        [Test]
        public void Authenticate_InputDtoAuthenticateUser_ThrowsWrongPasswordException()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            passwordEncryption
                .VerifyPassword(Arg.Any<IUser>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(false);
            
            userRep.Authenticate(Arg.Any<IUser>())
                .Returns(CreateUser(1));

            var userService = new UserService(userRep, addressService, passwordEncryption);
            
            // ASSERT //
            Assert.Throws<WrongPasswordException>(() => userService.Authenticate(CreateInputDtoAuthenticateUser(1)));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Delete_SingleNumber_ReturnsIsDeleted(bool isDeletedFromRepo)
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            userRep.Delete(1).Returns(isDeletedFromRepo);

            var userService = new UserService(userRep, addressService, passwordEncryption);
            
            // ACT //
            var output = userService.Delete(1);
            
            // ASSERT //
            Assert.AreEqual(output, isDeletedFromRepo);
        }

        [Test]
        public void RegisterAlreadyExistingAddress_UserIdAndInputDtoAddAddress_ReturnsOutputDtoQueryAddress()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            addressService.CheckFromDb(Arg.Any<InputDtoAddAddress>())
                .Returns(CreateOutputDtoQueryAddress(1));
            
            userRep.RegisterAddress(Arg.Any<int>(), Arg.Any<IAddress>())
                .Returns(true);
            
            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoQueryAddress(1);

            // ACT //
            var output = userService.RegisterAddress(1, CreateInputDtoAddAddress(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void RegisterNotAlreadyExistingAddress_UserIdAndInputDtoAddAddress_ReturnsOutputDtoQueryAddress()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            addressService.CheckFromDb(Arg.Any<InputDtoAddAddress>())
                .ReturnsNull();

            addressService.Create(Arg.Any<InputDtoAddAddress>())
                .Returns(CreateOutputDtoQueryAddress(1));
            
            userRep.RegisterAddress(Arg.Any<int>(), Arg.Any<IAddress>())
                .Returns(true);
            
            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoQueryAddress(1);

            // ACT //
            var output = userService.RegisterAddress(1, CreateInputDtoAddAddress(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void RegisterAddress_UserIdAndInputDtoAddAddress_ThrowsExcpetion()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            addressService.CheckFromDb(Arg.Any<InputDtoAddAddress>())
                .ReturnsNull();

            addressService.Create(Arg.Any<InputDtoAddAddress>())
                .Returns(CreateOutputDtoQueryAddress(1));
            
            userRep.RegisterAddress(Arg.Any<int>(), Arg.Any<IAddress>())
                .Returns(false);
            
            var userService = new UserService(userRep, addressService, passwordEncryption);
            
            // ASSERT //
            Assert.Throws<CouldNotUpdateAddressException>(() =>
                userService.RegisterAddress(1, CreateInputDtoAddAddress(1)));
        }

        [Test]
        public void GetUserAddress_SingleNumber_ReturnsOutputDtoQueryAddress()
        {
            // ARRANGE //
            var userRep = Substitute.For<IUserRepository>();
            var addressService = Substitute.For<IAddressService>();
            var passwordEncryption = Substitute.For<IPasswordEncryption>();

            userRep.GetUserAddress(1).Returns(CreateAddress(1));
            
            var userService = new UserService(userRep, addressService, passwordEncryption);
            var expected = CreateOutputDtoQueryAddress(1);
            
            // ACT //
            var output = userService.GetUserAddress(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }
    }
}