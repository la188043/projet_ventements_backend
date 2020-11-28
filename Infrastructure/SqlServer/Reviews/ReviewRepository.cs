using Application.Repositories;
using Domain.Items;
using Domain.reviews;
using Domain.Users;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Reviews
{
    public class ReviewRepository:IReviewRepository
    {
        public IReview Create(IUser userv, IItem item, IReview review)
        {
            var Review = new Review
            {
                User = userv,
                Item = item
            };
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ReviewSqlServer.ReqCreate;
                
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColStars}", review.Stars);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColLikes}", review.Likes);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColTitle}", review.Title);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColDescriptionReview}", review.DescriptionReview);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColItemId}", item.Id);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColUserId}", userv.Id);

                Review.Id = (int) cmd.ExecuteScalar();
            }

            return Review;
        }
        public bool Delete(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = ReviewSqlServer.ReqDelete;
                
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColId}", id);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(int id, IReview review)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = ReviewSqlServer.ReqPut;

                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColId}", id);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColStars}", review.Stars);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColDescriptionReview}", review.DescriptionReview);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}