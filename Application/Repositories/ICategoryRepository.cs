using System.Collections.Generic;
using Domain.Categories;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<ICategory> Query();
        ICategory GetById(int id);
        ICategory CreateCategory(ICategory category);
        ICategory CreateSubCategory(int parentCategoryId, ICategory childCategory);
    }
}