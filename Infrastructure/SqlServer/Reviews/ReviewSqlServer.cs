namespace Infrastructure.SqlServer.Reviews
{
    public class ReviewSqlServer
    {
        public static readonly string TableName = "review";
        public static readonly string ColId = "id";
        public static readonly string ColStars = "stars";
        public static readonly string ColLikes = "likes";
        public static readonly string ColTitle = "title";
        public static readonly string ColDescriptionReview = "descriptionReview";
        public static readonly string ColItemId = "itemId";
        public static readonly string ColUserId = "uservId";
        
        public static readonly string ReqCreate = $@"INSERT INTO {TableName} ({ColStars}, {ColLikes}, {ColTitle}, {ColDescriptionReview}, {ColItemId}, {ColUserId}) OUTPUT 
        Inserted.{ColId} VALUES (@{ColStars}, @{ColLikes}, @{ColTitle}, @{ColDescriptionReview}, @{ColItemId}, @{ColUserId})";
        
        public static readonly string ReqDelete = $@"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        
        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColStars} = @{ColStars},
            {ColDescriptionReview} = @{ColDescriptionReview}
            WHERE {ColId} = @{ColId}
        ";
    }
}