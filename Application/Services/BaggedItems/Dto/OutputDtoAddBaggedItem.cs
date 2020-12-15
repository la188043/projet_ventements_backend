using System;

namespace Application.Services.BaggedItems.Dto
{
    public class OutputDtoAddBaggedItem
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public Item BagItem { get; set; }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public float Price { get; set; }
            public string ImageItem { get; set; }
            public string DescriptionItem { get; set; }
        }

        protected bool Equals(OutputDtoAddBaggedItem other)
        {
            return Id == other.Id && AddedAt.Equals(other.AddedAt) && Quantity == other.Quantity &&
                   Size == other.Size && Equals(BagItem, other.BagItem);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoAddBaggedItem) obj);
        }
    }
}