

namespace Domain.Items
{
    public class ItemFactory : IItemFactory
    {
        public IItem CreateFromItemInformation(string label, float price, int quantity, string imageItem,
            string descriptionItem, string size,int subcategoryId)
        {
            return new Item
            {
                Label = label,
                Price = price,
                Quantity = quantity,
                ImageItem = imageItem,
                DescriptionItem = descriptionItem,
                Size = size,
                SubcategoryId = subcategoryId
            };
        }

    }
}