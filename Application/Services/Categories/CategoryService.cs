using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Categories.Dto;
using Domain.Categories;

namespace Application.Services.Categories
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<OutputDtoQueryCategory> Query()
        {
            return _categoryRepository
                .Query()
                .Select(category =>
                {
                    var subcategories = 
                        _categoryRepository.GetByCategoryId(category.Id)
                            .Select(subcategory => new OutputDtoQueryCategory.Category
                            {
                                Id = subcategory.Id,
                                Title = subcategory.Title
                            });
                    
                    return new OutputDtoQueryCategory
                    {
                        Id = category.Id,
                        Title = category.Title,
                        SubCategories = subcategories
                    };
                });
        }

        public OutputDtoQueryCategory GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            var subcategories = _categoryRepository.GetByCategoryId(id)
                .Select(subcategory => new OutputDtoQueryCategory.Category
                {
                    Id = subcategory.Id,
                    Title = subcategory.Title
                });

            return new OutputDtoQueryCategory
            {
                Id = category.Id,
                Title = category.Title,
                SubCategories = subcategories
            };
        }

        public IEnumerable<OutputDtoQueryCategory> GetByCategoryId(int parentCategoryId)
        {
            return _categoryRepository.GetByCategoryId(parentCategoryId)
                .Select(category =>
                {
                    return new OutputDtoQueryCategory
                    {
                        Id = category.Id,
                        Title = category.Title,
                    };
                });
        }

        public OutputDtoAddCategory CreateCategory(InputDtoAddCategory inputDtoAddCategory)
        {
            var categoryFromDto = new Category {Title = inputDtoAddCategory.Title};
            var categoryInDb = _categoryRepository.CreateCategory(categoryFromDto);

            return new OutputDtoAddCategory
            {
                Id = categoryInDb.Id,
                Title = categoryInDb.Title
            };
        }

        public OutputDtoAddCategory CreateSubCategory(int parentCategoryId, InputDtoAddCategory inputDtoAddCategory)
        {
            var categoryFromDto = new Category {Title = inputDtoAddCategory.Title};
            var categoryInDb = _categoryRepository.CreateSubCategory(parentCategoryId, categoryFromDto);
            
            return new OutputDtoAddCategory
            {
                Id = categoryInDb.Id,
                Title = categoryInDb.Title
            };
        }
    }
}