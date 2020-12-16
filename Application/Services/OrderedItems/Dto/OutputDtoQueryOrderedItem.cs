namespace Application.Services.OrderedItems.Dto
{
    public class OutputDtoQueryOrderedItem
    {
        public int Id { get; set; }
        public Order ItemOrder { get; set; }
        public Item ItemOrdered { get; set; }

        public class Order
        {
            public int Id { get; set; }
        }

        public class Item
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public float Price { get; set; }
            public string ImageItem { get; set; }
            public string DescriptionItem { get; set; }
            public int Quantity { get; set; }
            public string Size { get; set; }

            private bool Equals(Item other)
            {
                return Id == other.Id && Label == other.Label && Price.Equals(other.Price) &&
                       ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem &&
                       Quantity == other.Quantity && Size == other.Size;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Item) obj);
            }
        }

        private bool Equals(OutputDtoQueryOrderedItem other)
        {
            return Id == other.Id && ItemOrder.Equals(other.ItemOrder) && ItemOrdered.Equals(other.ItemOrdered);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoQueryOrderedItem) obj);
        }
    }
}