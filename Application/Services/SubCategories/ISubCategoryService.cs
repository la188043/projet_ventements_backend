using System.Collections.Generic;
using Application.Services.Categories.Dto;
using Application.Services.SubCategories.Dto;
using Domain.Categories;

namespace Application.Services.SubCategories
{
    public interface ISubCategoryService
    {
        IEnumerable<OutputDtoQuerySubCategory> Query();
        OutputDtoQuerySubCategory GetById(int id);
        OutputDtoQuerySubCategory Create(InputDtoAddSubCategory inputDtoAddSubCategory);
        bool Update(int id, InputDtoUpdateSubCategory inputDtoUpdateSubCategory);
        IEnumerable<OutputDtoQuerySubCategory> GetByCategoryId(int id);
    }
}