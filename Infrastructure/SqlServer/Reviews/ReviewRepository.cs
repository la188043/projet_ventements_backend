using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Items;
using Domain.reviews;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IInstanceFromReader<IReview> _factory = new ReviewFactory();
        public IEnumerable<IReview> Query()
        {
            IList<IReview> reviews = new List<IReview>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ReviewSqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    reviews.Add(_factory.CreateFromReader(reader));
            }

            return reviews;
        }

        public IEnumerable<IReview> GetByItemId(int itemId)
        {
            IList<IReview> reviews = new List<IReview>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ReviewSqlServer.ReqGetByItemId;

                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColItemId}", itemId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    reviews.Add(_factory.CreateFromReader(reader));
            }

            return reviews;
        }

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