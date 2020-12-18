using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Reviews.Dto;
using Domain.reviews;


namespace Application.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<OutputDtoQueryReview> Query()
        {
            return _reviewRepository
                .Query()
                .Select(review => new OutputDtoQueryReview
                {
                   Id = review.Id,
                    Stars = review.Stars,
                    Title = review.Title,
                    DescriptionReview = review.DescriptionReview,
                    Reviewer = new OutputDtoQueryReview.User
                    {
                        Id = review.Reviewer.Id,
                        Firstname = review.Reviewer.Firstname,
                        Lastname = review.Reviewer.Lastname
                    },
                    ItemReviewed = new OutputDtoQueryReview.Item
                    {
                        Id = review.ItemReviewed.Id,
                        Label = review.ItemReviewed.Label
                    }
                });
        }

        public OutputDtoQueryReview GetById(int id)
        {
            var reviewFromDb = _reviewRepository.GetById(id);
            
            return new OutputDtoQueryReview
            {
                Id = reviewFromDb.Id,
                Stars = reviewFromDb.Stars,
                Title = reviewFromDb.Title,
                DescriptionReview = reviewFromDb.DescriptionReview,
                Reviewer = new OutputDtoQueryReview.User
                {
                    Id = reviewFromDb.Reviewer.Id,
                    Firstname = reviewFromDb.Reviewer.Firstname,
                    Lastname = reviewFromDb.Reviewer.Lastname
                },
                ItemReviewed = new OutputDtoQueryReview.Item
                {
                    Id = reviewFromDb.ItemReviewed.Id,
                    Label = reviewFromDb.ItemReviewed.Label
                }
            };
        }

        public IEnumerable<OutputDtoQueryReview> GetByItemId(int itemId)
        {
            return _reviewRepository
                .GetByItemId(itemId)
                .Select(review => new OutputDtoQueryReview
                {
                    Id = review.Id,
                    Stars = review.Stars,
                    Title = review.Title,
                    DescriptionReview = review.DescriptionReview,
                    Reviewer = new OutputDtoQueryReview.User
                    {
                        Id = review.Reviewer.Id,
                        Firstname = review.Reviewer.Firstname,
                        Lastname = review.Reviewer.Lastname
                    },
                    ItemReviewed = new OutputDtoQueryReview.Item
                    {
                        Id = review.ItemReviewed.Id,
                        Label = review.ItemReviewed.Label
                    }
                });
        }

        public OutputDtoQueryReview Create(int uservId, int itemId, InputDtoAddReview review)
        {
            var reviewFromDb = _reviewRepository.Create(uservId, itemId, new Review
            {
                Stars = review.Stars,
                Title = review.Title,
                DescriptionReview = review.DescriptionReview
            });

            return GetById(reviewFromDb.Id);
        }

        public bool Delete(int id)
        {
            return _reviewRepository.Delete(id);
        }

        public bool Update(int id, InputDtoUpdateReview inputDtoUpdateReview)
        {
            var reviewFromDto = new Review
                {Stars = inputDtoUpdateReview.Stars, DescriptionReview = inputDtoUpdateReview.DescriptionReview};
            return _reviewRepository.Update(id, reviewFromDto);
        }
    }
}