using System.Collections.Generic;
using Application.Services.Categories.Dto;

  namespace Application.Services.Categories
{
    public interface ICategoryService
    {
        IEnumerable<OutputDtoQueryCategory> Query();
        OutputDtoAddCategory Create(InputDtoAddCategory inputDtoAddCategory);
        bool Update(int id, InputDtoUpdateCategory inputDtoUpdateCategory);
    }
}