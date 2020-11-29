using System.Collections.Generic;
using Application.Services.WishLists.Dto;
using Domain.Wishlists;

namespace Application.Services.WishLists
{
    public interface IWishListService
    {
        IEnumerable<OutputDtoQueryWishLists> Query();
        OutputDtoQueryWishLists GetById(int id);
        IEnumerable<OutputDtoQueryWishLists> GetByUserId(int uservId);
        OutputDtoQueryWishLists Add(int uservId, int itemId);
        bool Delete(int id);
    }
}