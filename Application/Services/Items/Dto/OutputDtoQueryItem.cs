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
        }
    }
}