using Domain.Items;
using Domain.Shared;
using Domain.Users;

namespace Domain.reviews
{
    public interface IReview : IEntity
    {
        public int Stars { get; set; }
        public string Title { get; set; }
        public string DescriptionReview { get; set; }
        IUser Reviewer { get; set; }
        IItem ItemReviewed { get; set; }
    }
}