using System.Collections.Generic;
using Domain.Addresses;
using Domain.Items;
using Domain.Users;
using Domain.Wishlists;

namespace Application.Repositories
{
    public interface IWishListRepository
    {
        IEnumerable<IWishlist> Query();
        IEnumerable<IWishlist> GetByUserId(int uservId);
        IWishlist Add(int uservId, int itemId);
        bool Delete(int id);
    }
}