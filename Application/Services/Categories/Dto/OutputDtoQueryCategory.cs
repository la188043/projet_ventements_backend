using System.Collections.Generic;

namespace Application.Services.Categories.Dto
{
    public class OutputDtoQueryCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}