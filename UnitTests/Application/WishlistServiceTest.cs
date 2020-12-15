using System;
using System.Collections.Generic;
using Application.Repositories;
using Application.Services.WishLists;
using Application.Services.WishLists.Dto;
using Domain.Categories;
using Domain.Exceptions;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class WishlistServiceTest
    {
        public static IWishlist CreateWishlist(int i)
        {
            return new WishList
            {
                Id = i,
                AddedAt = DateTime.Now,
                ItemWishList = new Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = i,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString(),
                    Category = new Category {Id = i, Title = i.ToString()}
                },
                UserWishList = new User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString(),
                    Email = $"{i}@gmail.com",
                    Birthdate = DateTime.Now,
                    EncryptedPassword = i.ToString(),
                    Administrator = false,
                    Gender = 'M',
                }
            };
        }

        public static IEnumerable<IWishlist> CreateListOfWishlist(int sizeOfList)
        {
            IList<IWishlist> wishlists = new List<IWishlist>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                wishlists.Add(CreateWishlist(i));
            }

            return wishlists;
        }

        public static OutputDtoQueryWishLists CreateOutputDtoQueryWishlist(int i)
        {
            return new OutputDtoQueryWishLists
            {
                Id = i,
                AddedAt = DateTime.Now,
                ItemWishList = new OutputDtoQueryWishLists.Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = i,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString()
                },
                UserWishList = new OutputDtoQueryWishLists.User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString()
                }
            };
        }

        public static IEnumerable<OutputDtoQueryWishLists> CreateListOfOutputDtoQueryWishlist(int sizeOfList)
        {
            IList<OutputDtoQueryWishLists> wishlists = new List<OutputDtoQueryWishLists>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                wishlists.Add(CreateOutputDtoQueryWishlist(i));
            }

            return wishlists;
        }

        [Test]
        public void GetById_SingleNumber_ReturnsSingleOutputDtoQueryWishlist()
        {
            // ARRANGE //
            var wishlistRep = Substitute.For<IWishListRepository>();
            wishlistRep.GetById(1).Returns(CreateWishlist(1));

            var wishlistService = new WishListService(wishlistRep);
            var expected = CreateOutputDtoQueryWishlist(1);

            // ACT //
            var output = wishlistService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(23)]
        public void GetByUserId_SingleNumber_ReturnsListOfOutputDtoQueryWishlist(int nbOfWishlistItems)
        {
            // ARRANGE //
            var wishlistRep = Substitute.For<IWishListRepository>();
            wishlistRep.GetByUserId(1).Returns(CreateListOfWishlist(nbOfWishlistItems));

            var wishlistService = new WishListService(wishlistRep);
            var expected = CreateListOfOutputDtoQueryWishlist(nbOfWishlistItems);

            // ACT //
            var output = wishlistService.GetByUserId(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Add_UserIdAndItemId_ReturnsOutputDtoQueryWishlist()
        {
            // ARRANGE //
            var wishlistRep = Substitute.For<IWishListRepository>();
            wishlistRep.GetById(1).Returns(CreateWishlist(1));
            wishlistRep.Add(1, 1).Returns(CreateWishlist(1));

            var wishlistService = new WishListService(wishlistRep);
            var expected = CreateOutputDtoQueryWishlist(1);

            // ACT //
            var output = wishlistService.Add(1, 1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Add_UserIdAndItemId_ThrowsExcpetion()
        {
            // ARRANGE //
            var wishlistRep = Substitute.For<IWishListRepository>();
            wishlistRep.GetById(1).Returns(CreateWishlist(1));
            wishlistRep.Add(1, 1)
                .Returns(x => { throw new DuplicateSqlPrimaryException("message"); });

            var wishlistService = new WishListService(wishlistRep);
            
            // ASSERT //
            Assert.Throws<DuplicateSqlPrimaryException>(() => wishlistService.Add(1, 1));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Delete_SingleNumber_ReturnsIsDeleted(bool isDeletedFromRepo)
        {
            // ARRANGE //
            var wishlistRep = Substitute.For<IWishListRepository>();
            wishlistRep.Delete(1).Returns(isDeletedFromRepo);

            var wishlistService = new WishListService(wishlistRep);
            
            // ACT //
            var output = wishlistService.Delete(1);

            // ASSERT //
            Assert.AreEqual(isDeletedFromRepo, output);
        }
    }
}