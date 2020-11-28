using System.Collections.Generic;
using Domain.Items;


namespace Application.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<IItem> Query();
        IItem GetById(int id);
        IItem Create(int subcategoryId, IItem item);
        IEnumerable<IItem> GetByCategoryId(int id);
    }
}