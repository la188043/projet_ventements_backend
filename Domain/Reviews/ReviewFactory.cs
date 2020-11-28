namespace Domain.reviews
{
    public class ReviewFactory : IReviewFactory
    {
        public IReview Update(int stars, string descriptionReview)
        {
            return new Review
            {
                Stars = stars,
                DescriptionReview = descriptionReview
            };
        }
    }
}