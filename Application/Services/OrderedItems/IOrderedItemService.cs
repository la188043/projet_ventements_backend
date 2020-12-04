using System.Collections.Generic;
using Application.Services.OrderedItems.Dto;
using Domain.OrderedItems;

namespace Application.Services.OrderedItems
{
    public interface IOrderedItemService
    {
        IEnumerable<OutputDtoQueryOrderedItem> GetByOrderId(int orderId);
        IOrderedItem AddItemToOrder(int orderId, int itemId, InputDtoAddOrderedItem inputDtoAddOrderedItem);
    }
}