using System;
using Domain.Items;
using Domain.Users;

namespace Domain.Wishlists
{
    public class WishList : IWishlist
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public IUser UserWishList { get; set; }
        public IItem ItemWishList { get; set; }
    }
}