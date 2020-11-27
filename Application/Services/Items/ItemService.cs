using System;
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
        private readonly ICategoryRepository _categoryRepository;

        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<OutputDtoQueryItem> Query()
        {
            return _itemRepository
                .Query()
                .Select(item =>
                {
                    var category = new OutputDtoQueryItem.Category
                    {
                        Id = item.Category.Id,
                        Title = item.Category.Title
                    };

                    return new OutputDtoQueryItem
                    {
                        Id = item.Id,
                        Label = item.Label,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        ImageItem = item.ImageItem,
                        DescriptionItem = item.DescriptionItem,
                        Size = item.Size,
                    };
                });
        }

        public OutputDtoQueryItem GetById(int id)
        {
            var item = _itemRepository.GetById(id);

            var category = new OutputDtoQueryItem.Category
            {
                Id = item.Category.Id,
                Title = item.Category.Title
            };

            var subcategory = new OutputDtoQueryItem.Category
            {
                Id = item.Category.Id,
                Title = item.Category.Title
            };
            
            return new OutputDtoQueryItem
            {
                Id = item.Id,
                Label = item.Label,
                Price = item.Price,
                Quantity = item.Quantity,
                ImageItem = item.ImageItem,
                DescriptionItem = item.DescriptionItem,
                Size = item.Size,
            };
        }

        public OutputDtoQueryItem Create(int subcategoryId, InputDtoAddItem inputDtoAddItem)
        {
            var subcategoryFromDto = _categoryRepository.GetById(subcategoryId);

            var itemFromDto = new Item
            {
                Label = inputDtoAddItem.Label,
                Price = inputDtoAddItem.Price,
                Quantity = inputDtoAddItem.Quantity,
                ImageItem = inputDtoAddItem.ImageItem,
                DescriptionItem = inputDtoAddItem.DescriptionItem,
                Size = inputDtoAddItem.Size,
                Category = subcategoryFromDto
            };

            var itemInDb = _itemRepository.Create(subcategoryId, itemFromDto);

            var category = new OutputDtoQueryItem.Category
            {
                Id = itemInDb.Category.Id,
                Title = itemInDb.Category.Title
            };

            var subcategory = new OutputDtoQueryItem.Category
            {
                Id = itemInDb.Category.Id,
                Title = itemInDb.Category.Title
            };

            return new OutputDtoQueryItem
            {
                Id = itemInDb.Id,
                Label = itemInDb.Label,
                Price = itemInDb.Price,
                Quantity = itemInDb.Quantity,
                ImageItem = itemInDb.ImageItem,
                DescriptionItem = itemInDb.DescriptionItem,
                Size = itemInDb.Size,
            };
        }

        /*
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
            };

            return _itemRepository.Update(id, itemFromDto);
        }
        */


        public IEnumerable<OutputDtoQueryItem> GetByCategoryId(int subcategoryId)
        {
            /*
            return _itemRepository.GetByCategoryId(subcategoryId)
                .Select(item =>
                {
                    var category = new OutputDtoQueryItem.Category
                    {
                        Id = item.Category.Category.Id,
                        Title = item.Category.Category.Title
                    };
                    
                    return new OutputDtoQueryItem
                    {
                        Id = item.Id,
                        Label = item.Label,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        ImageItem = item.ImageItem,
                        DescriptionItem = item.DescriptionItem,
                        Size = item.Size,
                    };
                });
                */

            throw new NotImplementedException();
        }
    }
}