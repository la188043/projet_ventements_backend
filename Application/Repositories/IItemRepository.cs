using System.Collections.Generic;
using Domain.Items;


namespace Application.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<IItem> Query();
        IItem GetById(int id);
        IItem Create(int subcategoryId, IItem item);
        bool Update(int id, IItem item);
        IEnumerable<IItem> GetBySubCategoryId(int id);
    }
}