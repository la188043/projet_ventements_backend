using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Reviews.Dto;
using Domain.reviews;


namespace Application.Services.Reviews
{
    public class ReviewService:IReviewService
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
                    Likes = review.Likes,
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

        public IEnumerable<OutputDtoQueryReview> GetByItemId(int itemId)
        {
            return _reviewRepository
                .GetByItemId(itemId)
                .Select(review => new OutputDtoQueryReview
                {
                    Id = review.Id,
                    Stars = review.Stars,
                    Likes = review.Likes,
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
            var userv = _userRepository.GetById(uservId);
            var item = _itemRepository.GetById(itemId);
            
            var reviewFromDb = _reviewRepository.Create(userv, item, new Review
            {
                Stars = review.Stars,
                Likes = review.Likes,
                Title = review.Title,
                DescriptionReview = review.DescriptionReview
            });
            
           return new OutputDtoQueryReview
            {
                Id = reviewFromDb.Id,
                Stars = review.Stars,
                Likes = review.Likes,
                Title = review.Title,
                DescriptionReview = review.DescriptionReview ,
                User=userv,
                Item=item
            };
        }

        public bool Delete(int id)
        {
            return _reviewRepository.Delete( id);
        }

        public bool Update(int id,InputDtoUpdateReview inputDtoUpdateReview)
        {
            var reviewFromDto = _reviewFactory.Update(inputDtoUpdateReview.Stars,inputDtoUpdateReview.DescriptionReview);
            return _reviewRepository.Update(id, reviewFromDto);
        }
    }
}