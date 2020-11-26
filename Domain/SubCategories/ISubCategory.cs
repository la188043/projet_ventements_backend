using Domain.Shared;

namespace Domain.SubCategories
{
    public interface ISubCategory : IEntity
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}