using System;
using System.Collections.Generic;
using Application.Repositories;
using Domain.Addresses;
using Domain.BaggedItems;
using Domain.Categories;
using Domain.Items;
using Domain.Users;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class BaggedItemServiceTest
    {
        // Helper static methods
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
                },
            };
        }

        public static IItem CreateItem(int i)
        {
            return new Item
            {
                Id = i,
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString(),
                Category = new Category {Id = i}
            };
        }

        public static IBaggedItem CreateBaggedItem(int i)
        {
            return new BaggedItem
            {
                 Id = i,
                 Quantity = i,
                 Size = i.ToString(),
                 AddedAt = DateTime.Now,               
                 BagOwner = CreateUser(i),
                 AddedItem = CreateItem(i)
            };
        }

        public static IEnumerable<IBaggedItem> CreateListOfBaggedItems(int listSize)
        {
            IList<IBaggedItem> baggedItems = new List<IBaggedItem>();
            for (var i = 1; i <= listSize; i++)
            {
                baggedItems.Add(CreateBaggedItem(i));
            }

            return baggedItems;
        }

        [Test]
        public void GetByUserId_SingleNumber_ReturnsOutputDtoQueryUserBaggedItem()
        {
            // ARRANGE //

            // Substitutes
            var userRep = Substitute.For<IUserRepository>();
            var itemRep = Substitute.For<IItemRepository>();
            var baggedItemRep = Substitute.For<IBaggedItemRepository>();

            // Substitutes behavior
            

            // ACT //

            // ASSERT //
        }
    }
}