using Domain.Categories;

namespace Application.Services.SubCategories.Dto
{
    public class OutputDtoQuerySubCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category ParentCategory { get; set; }
        public class Category
        {
            public string Title { get; set; }
        }
    }
}