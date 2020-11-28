using System.Data.SqlClient;
using Domain.Items;
using Domain.reviews;
using Domain.Users;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Reviews;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factories
{
    public class ReviewFactory : IInstanceFromReader<IReview>
    {
        public IReview CreateFromReader(SqlDataReader reader)
        {
            return new Review
            {
                Id = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColId)),
                Stars = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColStars)),
                Likes = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColLikes)),
                Title = reader.GetString(reader.GetOrdinal(ReviewSqlServer.ColTitle)),
                DescriptionReview = reader.GetString(reader.GetOrdinal(ReviewSqlServer.ColDescriptionReview)),

                Reviewer = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColUserId)),
                    Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                    Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname))
                },
                
                ItemReviewed = new Item
                {
                    Id = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColItemId)),
                    Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel))
                }
            };
        }
    }
}