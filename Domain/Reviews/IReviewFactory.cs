﻿namespace Domain.reviews
{
    public interface IReviewFactory
    {
        IReview Update(int stars, string descriptionReview);
    }
}