using System.Collections.Generic;
using Domain.BaggedItems;

namespace Application.Repositories
{
    public interface IBaggedItemRepository
    {
        IEnumerable<IBaggedItem> GetByUserId(int userId);
        IBaggedItem AddToBag(int userId, int itemId, IBaggedItem baggedItem);
        int EmptyBag(int userId);
        bool DeleteItem(int baggedItemId);
        bool UpdateQuantity(int baggedItemId, IBaggedItem baggedItem);
    }
}