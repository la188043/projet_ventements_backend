using System.Collections.Generic;
using Domain.SubCategories;

namespace Application.Repositories
{
    public interface ISubCategoryRepository
    {
        // IEnumerable<ISubCategory> Query();
        ISubCategory GetById(int id);
        ISubCategory Create(int categoryId, ISubCategory subCategory);
        // bool Update(int id, ISubCategory subCategory);
        IEnumerable<ISubCategory> GetByCategoryId(int id);
    }
}