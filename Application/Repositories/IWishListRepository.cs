using System.Collections.Generic;
using Domain.Wishlists;

namespace Application.Repositories
{
    public interface IWishListRepository
    {
        IWishlist GetById(int id);
        IEnumerable<IWishlist> GetByUserId(int uservId);
        IWishlist Add(int uservId, int itemId);
        bool Delete(int id);
    }
}