using System.Collections.Generic;
using Application.Services.Items.Dto;

namespace Application.Services.Items
{
    public interface IItemService
    {
        IEnumerable<OutputDtoQueryItem> Query();
        OutputDtoQueryItem GetById(int id);
        OutputDtoQueryItem Create(int subcategoryId, InputDtoAddItem inputDtoAddItem);
        // bool Update(int id, InputDtoUpdateItem inputDtoUpdateItem);
        IEnumerable<OutputDtoQueryItem> GetBySubCategoryId(int id);
    }
}