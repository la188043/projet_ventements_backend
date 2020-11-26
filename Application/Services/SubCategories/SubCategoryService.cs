using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.SubCategories.Dto;
using Domain.SubCategories;

namespace Application.Services.SubCategories
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        /*
        public IEnumerable<OutputDtoQuerySubCategory> Query()
        {
            return _subCategoryRepository
                .Query()
                .Select(subCategory =>
                {
                    var category = new OutputDtoQuerySubCategory.Category
                    {
                        Title = subCategory.Category.Title
                    };
                    
                    return new OutputDtoQuerySubCategory
                    {
                        Id = subCategory.Id,
                        Title = subCategory.Title
                    };
                });
        }
        */

        public OutputDtoQuerySubCategory GetById(int id)
        {
            var subcategory = _subCategoryRepository.GetById(id);

            var category = new OutputDtoQuerySubCategory.Category
            {
                Title = subcategory.Category.Title
            };
            
            return new OutputDtoQuerySubCategory
            {
                Id = subcategory.Id,
                Title = subcategory.Title,
            };
        }

        public OutputDtoQuerySubCategory Create(int categoryId, InputDtoAddSubCategory inputDtoAddSubCategory)
        {
            var categoryFromDto = _categoryRepository.GetById(categoryId);
            var subCategoryFromDto = new SubCategory
            {
                Title = inputDtoAddSubCategory.Title,
                Category = categoryFromDto
            };
            
            var subCategoryInDb = _subCategoryRepository.Create(categoryId, subCategoryFromDto);

            var category = new OutputDtoQuerySubCategory.Category
            {
                Title = subCategoryInDb.Category.Title
            };
            
            return new OutputDtoQuerySubCategory
            {
                Id = subCategoryInDb.Id,
                Title = subCategoryInDb.Title,
            };
        }

        public IEnumerable<OutputDtoQuerySubCategory> GetByCategoryId(int categoryId)
        {
            return _subCategoryRepository.GetByCategoryId(categoryId)
                .Select(subCategory =>
                {
                    var category = new OutputDtoQuerySubCategory.Category
                    {
                        Title = subCategory.Category.Title
                    };                   
                    
                    return new OutputDtoQuerySubCategory
                    {
                        Id = subCategory.Id,
                        Title = subCategory.Title,
                    };
                });
        }
    }
}