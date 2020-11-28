using System.Collections.Generic;
using Domain.Categories;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<ICategory> Query();
        ICategory GetById(int id);
        IEnumerable<ICategory> GetByCategoryId(int parentCategoryId);
        ICategory CreateCategory(ICategory category);
        ICategory CreateSubCategory(int parentCategoryId, ICategory childCategory);
    }
}