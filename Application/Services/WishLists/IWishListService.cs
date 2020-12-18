using System.Collections.Generic;
using Application.Services.WishLists.Dto;

namespace Application.Services.WishLists
{
    public interface IWishListService
    {
        OutputDtoQueryWishLists GetById(int id);
        IEnumerable<OutputDtoQueryWishLists> GetByUserId(int uservId);
        OutputDtoQueryWishLists Add(int uservId, int itemId);
        bool Delete(int id);
    }
}