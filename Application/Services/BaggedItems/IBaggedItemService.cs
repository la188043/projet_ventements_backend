using Application.Services.BaggedItems.Dto;

namespace Application.Services.BaggedItems
{
    public interface IBaggedItemService
    {
        OutputDtoQueryUserBaggedItem GetByUserId(int userId);
        OutputDtoAddBaggedItem AddToBag(int userId, int itemId, InputDtoAddItemToBag inputDtoAddItemToBag);
        int EmptyBag(int userId);
        bool DeleteItem(int baggedItemId);
        bool UpdateQuantity(int baggedItemId, InputDtoUpdateBaggedItem inputDtoUpdateBaggedItem);
    }
}