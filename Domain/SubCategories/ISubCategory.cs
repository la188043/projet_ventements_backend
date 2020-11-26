using Domain.Categories;
using Domain.Shared;

namespace Domain.SubCategories
{
    public interface ISubCategory : IEntity
    {
        public string Title { get; set; }
        public ICategory Category { get; set; }
    }
}