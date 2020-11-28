using Domain.Items;
using Domain.reviews;
using Domain.Users;

namespace Application.Repositories
{
    public interface IReviewRepository
    {
        IReview Create(IUser uservId, IItem itemId, IReview review);
        bool Delete(int id);
        bool Update(int id, IReview review);
    }
}