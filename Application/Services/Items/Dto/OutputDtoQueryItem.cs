namespace Application.Services.Items.Dto
{
    public class OutputDtoQueryItem
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public float Price { get; set; }
        public string ImageItem { get; set; }
        public string DescriptionItem { get; set; }
        public Category ItemCategory { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Title { get; set; }

            private bool Equals(Category other)
            {
                return Id == other.Id && Title == other.Title;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((Category) obj);
            }
        }

        private bool Equals(OutputDtoQueryItem other)
        {
            return Id == other.Id && Label == other.Label && Price.Equals(other.Price) &&
                   ImageItem == other.ImageItem && DescriptionItem == other.DescriptionItem &&
                   ItemCategory.Equals(other.ItemCategory);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((OutputDtoQueryItem) obj);
        }
    }
}