using System;
using System.Collections.Generic;
using Domain.Items;
using Domain.Users;

namespace Application.Services.WishLists.Dto
{
    public class OutputDtoQueryWishLists
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public User UserWishList { get; set; }
        public Item ItemWishList { get; set; }


        public class User
        {
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }
        }
    }
}