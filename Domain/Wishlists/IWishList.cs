using System;
using Domain.Items;
using Domain.Shared;
using Domain.Users;

namespace Domain.Wishlists
{
    public interface IWishlist : IEntity
    {
        public DateTime AddedAt { get; set; }
        IUser UserWishList { get; set; }
        IItem ItemWishList { get; set; }
    }
}