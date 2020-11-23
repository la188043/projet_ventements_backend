using Domain;
using Domain.Categories;

namespace Domains.Categories
{
    public class CategoryFactory : ICategoryFactory
    {
        public ICategory CreateFromTitle(string title)
        {
            return new Category
            {
                Title = title
            };
        }
    }
}