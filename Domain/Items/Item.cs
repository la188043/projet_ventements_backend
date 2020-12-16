using Domain.Categories;

namespace Domain.Items
{
    public class Item : IItem
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public float Price { get; set; }
        public string ImageItem { get; set; }
        public string DescriptionItem { get; set; }
        public ICategory Category { get; set; }

        public Item()
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is Item item)
            {
                return Id == item.Id;
            }

            return false;
        }
    }
}