using System.Collections.Generic;
using Application.Services.Reviews.Dto;


namespace Application.Services.Reviews
{
    public interface IReviewService
    {
        OutputDtoQueryReview GetById(int id);
        IEnumerable<OutputDtoQueryReview> GetByItemId(int itemId);
        OutputDtoQueryReview Create(int uservId, int itemId, InputDtoAddReview review);
        bool Delete(int id);
        bool Update(int id, InputDtoUpdateReview inputDtoUpdateReview);
    }
}