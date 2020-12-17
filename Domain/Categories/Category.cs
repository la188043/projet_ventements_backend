namespace Domain.Categories
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICategory ParentCategory { get; set; }
    }
}