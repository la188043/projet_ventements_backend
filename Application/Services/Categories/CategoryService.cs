using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Categories.Dto;
using Domain;
using Domain.Categories;
using Domains.Categories;

namespace Application.Services.Categories
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryFactory _categoryFactory = new CategoryFactory();

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<OutputDtoQueryCategory> Query()
        {
            return _categoryRepository
                .Query()
                .Select(category => new OutputDtoQueryCategory
                {
                    Id = category.Id,
                    Title = category.Title,
                });
        }

        public OutputDtoAddCategory Create(InputDtoAddCategory inputDtoAddCategory)
        {
            var categoryFromDto = _categoryFactory.CreateFromTitle(inputDtoAddCategory.Title);
            var categoryInDb = _categoryRepository.Create(categoryFromDto);

            return new OutputDtoAddCategory
            {
                Id = categoryInDb.Id,
                Title = categoryInDb.Title
            };
        }

        public bool Update(int id, InputDtoUpdateCategory inputDtoUpdateCategory)
        {
            var categoryFromDto = _categoryFactory.CreateFromTitle(inputDtoUpdateCategory.Title);
            return _categoryRepository.Update(id, categoryFromDto);
        }
    }
}