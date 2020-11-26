using Domain.Shared;
using Domain.SubCategories;


namespace Domain.Items
{
    public interface IItem : IEntity
    {
        string Label { get; set; }
        float Price { get; set; }
        int Quantity { get; set; }
        string ImageItem { get; set; }
        string DescriptionItem { get; set; }
        string Size { get; set; }
        // int SubcategoryId { get; set; }
        ISubCategory SubCategory { get; set; }
    }
}