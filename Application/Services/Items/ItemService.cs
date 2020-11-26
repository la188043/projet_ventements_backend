using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Items.Dto;
using Application.Services.SubCategories.Dto;
using Domain.Items;

namespace Application.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public ItemService(IItemRepository itemRepository, ISubCategoryRepository subCategoryRepository)
        {
            _itemRepository = itemRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<OutputDtoQueryItem> Query()
        {
            return _itemRepository
                .Query()
                .Select(item =>
                {
                    var category = new OutputDtoQueryItem.SubCategory.Category
                    {
                        Id = item.SubCategory.Category.Id,
                        Title = item.SubCategory.Category.Title
                    };

                    var subcategory = new OutputDtoQueryItem.SubCategory
                    {
                        Id = item.SubCategory.Id,
                        Title = item.SubCategory.Title
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

            var category = new OutputDtoQueryItem.SubCategory.Category
            {
                Id = item.SubCategory.Category.Id,
                Title = item.SubCategory.Category.Title
            };

            var subcategory = new OutputDtoQueryItem.SubCategory
            {
                Id = item.SubCategory.Id,
                Title = item.SubCategory.Title
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
            var subcategoryFromDto = _subCategoryRepository.GetById(subcategoryId);
            
            var itemFromDto = new Item
            {
                Label = inputDtoAddItem.Label,
                Price = inputDtoAddItem.Price,
                Quantity = inputDtoAddItem.Quantity,
                ImageItem = inputDtoAddItem.ImageItem,
                DescriptionItem = inputDtoAddItem.DescriptionItem,
                Size = inputDtoAddItem.Size,
                SubCategory = subcategoryFromDto
            };

            var itemInDb = _itemRepository.Create(subcategoryId, itemFromDto);

            var category = new OutputDtoQueryItem.SubCategory.Category
            {
                Id = itemInDb.SubCategory.Category.Id,
                Title = itemInDb.SubCategory.Category.Title
            };
            
            var subcategory = new OutputDtoQueryItem.SubCategory
            {
                Id = itemInDb.SubCategory.Id,
                Title = itemInDb.SubCategory.Title
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


        public IEnumerable<OutputDtoQueryItem> GetBySubCategoryId(int subcategoryId)
        {
            return _itemRepository.GetBySubCategoryId(subcategoryId)
                .Select(item =>
                {
                    var category = new OutputDtoQueryItem.SubCategory.Category
                    {
                        Id = item.SubCategory.Category.Id,
                        Title = item.SubCategory.Category.Title
                    };
                    
                    var subcategory = new OutputDtoQueryItem.SubCategory
                    {
                        Id = item.SubCategory.Id,
                        Title = item.SubCategory.Title
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
    }
}