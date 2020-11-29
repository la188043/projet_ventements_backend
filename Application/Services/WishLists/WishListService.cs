﻿using System;
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
        

        public WishListService(IWishListRepository wishListRepository,IItemRepository itemRepository,IUserRepository userRepository)
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
                        Label = wishList.ItemWishList.Label
                    }
                });
        }

        public OutputDtoQueryWishLists Add(int uservId, int itemId,InputDtoAddWishList wishList)
        {
            //var itemss = _itemRepository.GetById(itemId);
            var user = _userRepository.GetById(uservId);
            var wishlistFromDb = _wishListRepository.Add(uservId, itemId, new WishList
            {
              // AddedAt = wishList.AddedAt,
            });

            return new OutputDtoQueryWishLists
            {
                Id = wishlistFromDb.Id,
               AddedAt = wishList.AddedAt,
               //    ItemWishList = OutputDtoQueryWishLists.itemss,
           // UserWishList = OutputDtoQueryWishLists.user
            };
        }

        public bool Delete(int id)
        {
            return _wishListRepository.Delete(id);
        }
    }
}

