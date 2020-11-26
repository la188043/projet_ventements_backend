using Domain.Categories;

namespace Domain.SubCategories
{
    public class SubCategory : ISubCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICategory Category { get; set; }

        public SubCategory()
        {
        }
    }
}