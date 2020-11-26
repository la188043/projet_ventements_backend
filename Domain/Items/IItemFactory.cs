namespace Domain.Items
{
    public interface IItemFactory
    {
        IItem CreateFromItemInformation(string label, float price, int quantity, string imageItem,
            string descriptionItem, string size, int subcategoryId);
    }
}