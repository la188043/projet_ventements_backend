using System.Collections.Generic;
using Application.Services.Items.Dto;

namespace Application.Services.Items
{
    public interface IItemService
    {
        IEnumerable<OutputDtoQueryItem> Query();
        OutputDtoQueryItem GetById(int id);
        OutputDtoQueryItem Create(int categoryId, InputDtoAddItem inputDtoAddItem);
        IEnumerable<OutputDtoQueryItem> GetByCategoryId(int id);
    }
}