using System.Collections.Generic;
using Application.Repositories;
using Application.Services.OrderedItems.Dto;
using Domain.OrderedItems;

namespace Application.Services.OrderedItems
{
    public class OrderedItemService : IOrderedItemService
    {
        private IOrderedItemRepository _orderedItemRepository;

        public OrderedItemService(IOrderedItemRepository orderedItemRepository)
        {
            _orderedItemRepository = orderedItemRepository;
        }

        public IEnumerable<OutputDtoQueryOrderedItem> GetByOrderId(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoQueryOrderedItem GetById(int orderedItemId)
        {
            throw new System.NotImplementedException();
        }

        public OutputDtoQueryOrderedItem AddItemToOrder(int orderId, int itemId,
            InputDtoAddOrderedItem inputDtoAddOrderedItem)
        {
            var orderedItemId = _orderedItemRepository.AddItemToOrder(orderId, itemId,
                new OrderedItem {Quantity = inputDtoAddOrderedItem.Quantity});

            return GetById(orderedItemId.Id);
        }
    }
}