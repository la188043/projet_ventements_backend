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
                    OutputDtoQueryCategory.Category parentCategory = null;
                    if (category.ParentCategory != null)
                    {
                        parentCategory = new OutputDtoQueryCategory.Category
                        {
                            Id = category.ParentCategory.Id,
                            Title = category.ParentCategory.Title
                        };
                    }
                    
                    return new OutputDtoQueryCategory
                    {
                        Id = category.Id,
                        Title = category.Title,
                        ParentCategory = parentCategory
                    };
                });
        }

        public OutputDtoQueryCategory GetById(int id)
        {
            throw new System.NotImplementedException();
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