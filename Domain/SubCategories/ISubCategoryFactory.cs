using Domain;

namespace Domains.SubCategories
{
    public interface ISubCategoryFactory
    {
        ISubCategory CreateFromCategoryTitle(int categoryId ,string title);
    }
}