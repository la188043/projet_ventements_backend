using System;
using System.Collections.Generic;
using Application.Services.Users.Dto;
using Domain.Addresses;
using Domain.Users;
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
    }
}