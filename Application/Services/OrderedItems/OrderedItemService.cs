using System;
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
            throw new NotImplementedException();
        }

        public OutputDtoQueryOrderedItem GetById(int orderedItemId)
        {
            var orderedItemFromDb = _orderedItemRepository.GetById(orderedItemId);

            return new OutputDtoQueryOrderedItem
            {
                Id = orderedItemFromDb.Id,
                ItemOrder = new OutputDtoQueryOrderedItem.Order
                {
                    Id = orderedItemFromDb.Order.Id
                },
                ItemOrdered = new OutputDtoQueryOrderedItem.Item
                {
                    Id = orderedItemFromDb.ItemOrdered.Id,
                    Label = orderedItemFromDb.ItemOrdered.Label,
                    Price = orderedItemFromDb.ItemOrdered.Price,
                    ImageItem = orderedItemFromDb.ItemOrdered.ImageItem,
                    DescriptionItem = orderedItemFromDb.ItemOrdered.DescriptionItem,
                    Quantity = orderedItemFromDb.Quantity
                }
            };
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