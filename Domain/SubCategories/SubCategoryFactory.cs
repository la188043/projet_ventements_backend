using Domain;

namespace Domains.SubCategories
{
    public class SubCategoryFactory :ISubCategoryFactory
    {
     
        public ISubCategory CreateFromCategoryTitle(int categoryId ,string title)
        {
            return new SubCategory
            {
                CategoryId=categoryId,
                Title = title
            };
        }
    }
}