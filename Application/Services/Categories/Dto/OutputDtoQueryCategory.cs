using Domain.Categories;

namespace Application.Services.Categories.Dto
{
    public class OutputDtoQueryCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Category ParentCategory { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}