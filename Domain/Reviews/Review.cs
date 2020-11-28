using Domain.Items;
using Domain.Users;

namespace Domain.reviews
{
    public class Review : IReview
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public int Likes { get; set; }
        public string Title { get; set; }
        public string DescriptionReview { get; set; }
        public IUser Reviewer { get; set; }
        public IItem ItemReviewed { get; set; }
    }
}