using Domain.Categories;

namespace Application.Services.SubCategories.Dto
{
    public class OutputDtoQuerySubCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        // public int CategoryId { get; set; }
        public class Category
        {
            public string Title { get; set; }
        }
    }
}