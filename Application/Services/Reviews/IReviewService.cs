using Application.Services.Reviews.Dto;


namespace Application.Services.Reviews
{
    public interface IReviewService
    {
        OutputDtoAddReview Create(int uservId,int itemId,InputDtoAddReview review);
        bool Delete(int id);
        bool Update(int id, InputDtoUpdateReview inputDtoUpdateReview);
    }
}