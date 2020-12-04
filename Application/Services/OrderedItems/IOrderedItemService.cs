﻿using System.Collections.Generic;
using Application.Services.OrderedItems.Dto;
using Domain.OrderedItems;

namespace Application.Services.OrderedItems
{
    public interface IOrderedItemService
    {
        IEnumerable<OutputDtoQueryOrderedItem> GetByOrderId(int orderId);
        OutputDtoQueryOrderedItem GetById(int orderedItemId);
        OutputDtoQueryOrderedItem AddItemToOrder(int orderId, int itemId, InputDtoAddOrderedItem inputDtoAddOrderedItem);
        bool UpdateQuantity(int orderedItemId, InputDtoUpdateOrderedItem inputDtoUpdateOrderedItem);
        bool Delete(int orderedItemId);
    }
}