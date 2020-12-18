using System.Collections.Generic;
using Domain.reviews;

namespace Application.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<IReview> Query();
        IReview GetById(int id);
        IEnumerable<IReview> GetByItemId(int itemId);
        IReview Create(int uservId, int itemId, IReview review);
        bool Delete(int id);
        bool Update(int id, IReview review);
    }
}