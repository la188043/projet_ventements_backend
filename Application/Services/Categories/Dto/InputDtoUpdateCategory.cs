using Domain;
using Domain.Categories;

namespace Application.Services.Categories.Dto
{
    public class InputDtoUpdateCategory
    {
        public string Title { get; set; }
        public ICategory Category { get; set; }
    }
}