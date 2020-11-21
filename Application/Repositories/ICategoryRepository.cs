using System.Collections.Generic;

using Domain.Categories;

namespace Application.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<ICategory> Query();
        ICategory GetById(int id);
        ICategory Create(ICategory category);
        bool Update(int id, ICategory category);
        

    }

    
}