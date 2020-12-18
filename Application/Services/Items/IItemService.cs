using System.Collections.Generic;
using Application.Services.Items.Dto;

namespace Application.Services.Items
{
    public interface IItemService
    {
        IEnumerable<OutputDtoQueryItem> Query();
        OutputDtoQueryItem GetById(int id);
        IEnumerable<OutputDtoQueryItem> GetByCategoryId(int id);
        OutputDtoQueryItem Create(int categoryId, InputDtoAddItem inputDtoAddItem);
        bool Update(int id, InputDtoUpdateItem inputDtoUpdateItem);
        bool Delete(int id);
    }
}