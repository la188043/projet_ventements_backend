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
        }
    }
}