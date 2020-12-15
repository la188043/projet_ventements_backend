using System;
using System.Collections.Generic;
using Application.Services.WishLists.Dto;
using Domain.Categories;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;
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
    }
}