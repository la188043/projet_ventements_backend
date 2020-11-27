using System.Collections.Generic;
using Application.Services.Categories.Dto;

  namespace Application.Services.Categories
{
    public interface ICategoryService
    {
        IEnumerable<OutputDtoQueryCategory> Query();
        OutputDtoQueryCategory GetById(int id);
        IEnumerable<OutputDtoQueryCategory> GetByCategoryId(int parentCategoryId);
        OutputDtoAddCategory CreateCategory(InputDtoAddCategory inputDtoAddCategory);
        OutputDtoAddCategory CreateSubCategory(int parentCategoryId, InputDtoAddCategory inputDtoAddCategory);
    }
}