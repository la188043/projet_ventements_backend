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

            private bool Equals(Item other)
            {
                return Id == other.Id && Label == other.Label && Price.Equals(other.Price) &&
                       ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((Item) obj);
            }
        }

        private bool Equals(OutputDtoAddBaggedItem other)
        {
            return Id == other.Id && Quantity == other.Quantity &&
                   Size == other.Size && BagItem.Equals(other.BagItem);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OutputDtoAddBaggedItem) obj);
        }
    }
}