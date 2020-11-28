using System.Data.SqlClient;
using Domain.Items;
using Domain.reviews;
using Domain.Users;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Reviews;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factories
{
    public class ReviewFactory: IInstanceFromReader<IReview> 
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
                
                User =new User()
                {
                    Id = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColId)),
                    Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                    Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname)),
                    Birthdate = reader.GetDateTime(reader.GetOrdinal(UserSqlServer.ColBirthDate)),
                    Email = reader.GetString(reader.GetOrdinal(UserSqlServer.ColEmail)),
                    EncryptedPassword = reader.GetString(reader.GetOrdinal(UserSqlServer.ColPassword)),
                    Gender = reader.GetString(reader.GetOrdinal(UserSqlServer.ColGender))[0],
                    Administrator = reader.GetBoolean(reader.GetOrdinal(UserSqlServer.ColAdmin))
                },
                
                Item = new Item()
                {   
                    Id = reader.GetInt32(reader.GetOrdinal(ReviewSqlServer.ColId)),
                    Label = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColLabel)),
                    Price = (float) reader.GetDouble(reader.GetOrdinal(ItemSqlServer.ColPrice)),
                    Quantity = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColQuantity)),
                    ImageItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColImageItem)),
                    DescriptionItem = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColDescriptionItem)),
                    Size = reader.GetString(reader.GetOrdinal(ItemSqlServer.ColSize)),
                   // SubcategoryId = reader.GetInt32(reader.GetOrdinal(ItemSqlServer.ColSubCategoryId))
                }
              
                
            };
        }
    }
}