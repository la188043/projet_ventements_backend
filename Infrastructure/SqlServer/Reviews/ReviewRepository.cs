using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.reviews;
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

        public IReview GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ReviewSqlServer.ReqGetById;

                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
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

        public IReview Create(int uservId, int itemId, IReview review)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = ReviewSqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColStars}", review.Stars);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColTitle}", review.Title);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColDescriptionReview}", review.DescriptionReview);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColItemId}", itemId);
                cmd.Parameters.AddWithValue($"@{ReviewSqlServer.ColUserId}", uservId);

                review.Id = (int) cmd.ExecuteScalar();
            }

            return review;
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