using System.Collections.Generic;
using Application.Services.WishLists.Dto;
using Domain.Wishlists;

namespace Application.Services.WishLists
{
    public interface IWishListService
    {
        IEnumerable<OutputDtoQueryWishLists> Query();
        IEnumerable<OutputDtoQueryWishLists> GetByUserId(int uservId);
        OutputDtoQueryWishLists Add(int uservId, int itemId, InputDtoAddWishList wishList);
        bool Delete(int id);
    }
}