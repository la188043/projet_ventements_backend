using System.Collections.Generic;
using Domain.Items;


namespace Application.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<IItem> Query();
        IItem GetById(int id);
        IEnumerable<IItem> GetByCategoryId(int id);
        IItem Create(int categoryId, IItem item);
        public bool Update(int id, IItem item);
    }
}