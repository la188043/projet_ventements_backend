using Application.Repositories;
using Application.Services.Reviews.Dto;
using Domain.reviews;


namespace Application.Services.Reviews
{
    public class ReviewService:IReviewService
    {
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewFactory _reviewFactory= new ReviewFactory();

        public ReviewService(IUserRepository userRepository, IItemRepository itemRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _reviewRepository = reviewRepository;
        }
        public OutputDtoAddReview Create(int uservId, int itemId, InputDtoAddReview review)
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
            
           return new OutputDtoAddReview
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