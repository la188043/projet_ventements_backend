namespace Domain.SubCategories
{
    public interface ISubCategoryFactory
    {
        ISubCategory CreateFromCategoryTitle(int categoryId, string title);
    }
}