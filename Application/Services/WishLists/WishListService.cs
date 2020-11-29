using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.WishLists.Dto;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;

namespace Application.Services.WishLists
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;


        public WishListService(IWishListRepository wishListRepository, IItemRepository itemRepository,
            IUserRepository userRepository)
        {
            _wishListRepository = wishListRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<OutputDtoQueryWishLists> Query()
        {
            return _wishListRepository
                .Query()
                .Select(wishList => new OutputDtoQueryWishLists
                {
                    Id = wishList.Id,
                    AddedAt = wishList.AddedAt,
                    UserWishList = new OutputDtoQueryWishLists.User
                    {
                        Id = wishList.UserWishList.Id,
                        Firstname = wishList.UserWishList.Firstname,
                        Lastname = wishList.UserWishList.Lastname
                    },
                    ItemWishList = new OutputDtoQueryWishLists.Item
                    {
                        Id = wishList.ItemWishList.Id,
                        Label = wishList.ItemWishList.Label
                    }
                });
        }

        public OutputDtoQueryWishLists GetById(int id)
        {
            var wishlistFromDb = _wishListRepository.GetById(id);
            
            return new OutputDtoQueryWishLists
            {
                Id = wishlistFromDb.Id,
                AddedAt = wishlistFromDb.AddedAt,
                UserWishList = new OutputDtoQueryWishLists.User
                {
                    Id = wishlistFromDb.UserWishList.Id,
                    Firstname = wishlistFromDb.UserWishList.Firstname,
                    Lastname = wishlistFromDb.UserWishList.Lastname
                },
                ItemWishList = new OutputDtoQueryWishLists.Item
                {
                    Id = wishlistFromDb.ItemWishList.Id,
                    Label = wishlistFromDb.ItemWishList.Label,
                    Price = wishlistFromDb.ItemWishList.Price,
                    ImageItem = wishlistFromDb.ItemWishList.ImageItem,
                    DescriptionItem = wishlistFromDb.ItemWishList.DescriptionItem
                }
            };
        }

        public IEnumerable<OutputDtoQueryWishLists> GetByUserId(int uservId)
        {
            return _wishListRepository
                .GetByUserId(uservId)
                .Select(wishList => new OutputDtoQueryWishLists
                {
                    Id = wishList.Id,
                    AddedAt = wishList.AddedAt,
                    UserWishList = new OutputDtoQueryWishLists.User
                    {
                        Id = wishList.UserWishList.Id,
                        Firstname = wishList.UserWishList.Firstname,
                        Lastname = wishList.UserWishList.Lastname
                    },
                    ItemWishList = new OutputDtoQueryWishLists.Item
                    {
                        Id = wishList.ItemWishList.Id,
                        Label = wishList.ItemWishList.Label,
                        Price = wishList.ItemWishList.Price,
                        ImageItem = wishList.ItemWishList.ImageItem,
                        DescriptionItem = wishList.ItemWishList.DescriptionItem
                    }
                });
        }

        public OutputDtoQueryWishLists Add(int uservId, int itemId)
        {
            var wishlistFromAdd = _wishListRepository.Add(uservId, itemId);

            return GetById(wishlistFromAdd.Id);
        }

        public bool Delete(int id)
        {
            return _wishListRepository.Delete(id);
        }
    }
}