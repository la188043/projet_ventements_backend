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

        private readonly ISubCategoryFactory _subCategoryFactory = new SubCategoryFactory();

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public IEnumerable<OutputDtoQuerySubCategory> Query()
        {
            return _subCategoryRepository
                .Query()
                .Select(subCategory => new OutputDtoQuerySubCategory
                {
                    Id = subCategory.Id,
                    Title = subCategory.Title,
                    CategoryId = subCategory.CategoryId
                });
        }

        public OutputDtoQuerySubCategory GetById(int id)
        {
            var category = _subCategoryRepository.GetById(id);

            return new OutputDtoQuerySubCategory
            {
                Id = category.Id,
                Title = category.Title,
                CategoryId = category.CategoryId
            };
        }

        public OutputDtoQuerySubCategory Create(InputDtoAddSubCategory inputDtoAddSubCategory)
        {
            var subCategoryFromDto =
                _subCategoryFactory.CreateFromCategoryTitle(inputDtoAddSubCategory.CategoryId,
                    inputDtoAddSubCategory.Title);
            var subCategoryInDb = _subCategoryRepository.Create(subCategoryFromDto);

            return new OutputDtoQuerySubCategory 
            {
                Id = subCategoryInDb.Id,
                Title = subCategoryInDb.Title,
                CategoryId = subCategoryInDb.CategoryId
            };
        }

        public bool Update(int id, InputDtoUpdateSubCategory inputDtoUpdateSubCategory)
        {
            var categoryFromDto = _subCategoryFactory.CreateFromCategoryTitle(inputDtoUpdateSubCategory.CategoryId,
                inputDtoUpdateSubCategory.Title);
            return _subCategoryRepository.Update(id, categoryFromDto);
        }


        public IEnumerable<OutputDtoQuerySubCategory> GetByCategoryId(int id)
        {
            return _subCategoryRepository.GetByCategoryId(id)
                .Select(subCategory => new OutputDtoQuerySubCategory
                {
                    Id = subCategory.Id,
                    Title = subCategory.Title,
                    CategoryId = subCategory.CategoryId
                });
        }
    }
}