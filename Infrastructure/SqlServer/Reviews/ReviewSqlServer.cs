using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Reviews
{
    public class ReviewSqlServer
    {
        public static readonly string TableName = "review";
        public static readonly string ColId = "id";
        public static readonly string ColStars = "stars";
        public static readonly string ColTitle = "title";
        public static readonly string ColDescriptionReview = "descriptionReview";
        public static readonly string ColItemId = "itemId";
        public static readonly string ColUserId = "uservId";

        public static readonly string ReqQuery = $@"
            SELECT {TableName}.{ColId},
                   {TableName}.{ColStars},
                   {TableName}.{ColTitle},
                   {TableName}.{ColDescriptionReview},
                   {TableName}.{ColUserId},
                   {UserSqlServer.TableName}.{UserSqlServer.ColFirstname},
                   {UserSqlServer.TableName}.{UserSqlServer.ColLastname},
                   {TableName}.{ColItemId},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColLabel}
            FROM {TableName}
            INNER JOIN {UserSqlServer.TableName} 
            ON {TableName}.{ColUserId} = {UserSqlServer.TableName}.{UserSqlServer.ColId}
            INNER JOIN {ItemSqlServer.TableName}
            ON {TableName}.{ColItemId} = {ItemSqlServer.TableName}.{ItemSqlServer.ColId}
        ";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        public static readonly string ReqGetByItemId = ReqQuery + $" WHERE {TableName}.{ColItemId} = @{ColItemId}";
        
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName} ({ColStars}, {ColTitle}, {ColDescriptionReview}, {ColItemId}, {ColUserId}) 
            OUTPUT INSERTED.{ColId} 
            VALUES (@{ColStars}, @{ColTitle}, @{ColDescriptionReview}, @{ColItemId}, @{ColUserId})
        ";

        public static readonly string ReqDelete = $@"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";

        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColStars} = @{ColStars},
            {ColDescriptionReview} = @{ColDescriptionReview}
            WHERE {ColId} = @{ColId}
        ";
    }
}