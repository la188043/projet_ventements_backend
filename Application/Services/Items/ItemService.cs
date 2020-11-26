using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Items.Dto;
using Domain.Items;

namespace Application.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IEnumerable<OutputDtoQueryItem> Query()
        {
            return _itemRepository
                .Query()
                .Select(item => new OutputDtoQueryItem
                {
                    Id = item.Id,
                    Label = item.Label,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ImageItem = item.ImageItem,
                    DescriptionItem = item.DescriptionItem,
                    Size = item.Size,
                    SubcategoryId = item.SubcategoryId
                });
        }

        public OutputDtoQueryItem GetById(int id)
        {
            var item = _itemRepository.GetById(id);

            return new OutputDtoQueryItem
            {
                Id = item.Id,
                Label = item.Label,
                Price = item.Price,
                Quantity = item.Quantity,
                ImageItem = item.ImageItem,
                DescriptionItem = item.DescriptionItem,
                Size = item.Size,
                SubcategoryId = item.SubcategoryId
            };
        }

        public OutputDtoQueryItem Create(int subcategoryId, InputDtoAddItem inputDtoAddItem)
        {
           var itemFromDto = new Item
           {
               Label = inputDtoAddItem.Label,
               Price = inputDtoAddItem.Price,
               Quantity = inputDtoAddItem.Quantity,
               ImageItem = inputDtoAddItem.ImageItem,
               DescriptionItem = inputDtoAddItem.DescriptionItem,
               Size = inputDtoAddItem.Size,
               SubcategoryId = inputDtoAddItem.SubcategoryId
           }; 

            var ItemInDb = _itemRepository.Create(subcategoryId, itemFromDto);

            return new OutputDtoQueryItem
            {
                Id = ItemInDb.Id,
                Label = ItemInDb.Label,
                Price = ItemInDb.Price,
                Quantity = ItemInDb.Quantity,
                ImageItem = ItemInDb.ImageItem,
                DescriptionItem = ItemInDb.DescriptionItem,
                Size = ItemInDb.Size,
                SubcategoryId = ItemInDb.SubcategoryId
            };
        }

        public bool Update(int id, InputDtoUpdateItem inputDtoUpdateItem)
        {
           var itemFromDto = new Item
           {
               Label = inputDtoUpdateItem.Label,
               Price = inputDtoUpdateItem.Price,
               Quantity = inputDtoUpdateItem.Quantity,
               ImageItem = inputDtoUpdateItem.ImageItem,
               DescriptionItem = inputDtoUpdateItem.DescriptionItem,
               Size = inputDtoUpdateItem.Size,
               SubcategoryId = inputDtoUpdateItem.SubcategoryId
           }; 
           
            return _itemRepository.Update(id, itemFromDto);
        }


        public IEnumerable<OutputDtoQueryItem> GetBySubCategoryId(int subcategoryId)
        {
            return _itemRepository.GetBySubCategoryId(subcategoryId)
                .Select(item => new OutputDtoQueryItem
                {
                    Id = item.Id,
                    Label = item.Label,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ImageItem = item.ImageItem,
                    DescriptionItem = item.DescriptionItem,
                    Size = item.Size,
                    SubcategoryId = item.SubcategoryId
                });
        }
    }
}