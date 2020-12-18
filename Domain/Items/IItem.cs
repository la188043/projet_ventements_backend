using Domain.Categories;
using Domain.Shared;

namespace Domain.Items
{
    public interface IItem : IEntity
    {
        string Label { get; set; }
        float Price { get; set; }
        string ImageItem { get; set; }
        string DescriptionItem { get; set; }
        ICategory Category { get; set; }
    }
}