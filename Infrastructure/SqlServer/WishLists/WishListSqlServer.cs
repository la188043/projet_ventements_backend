using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.WishLists
{
    public class WishListSqlServer
    {
        public static readonly string TableName = "wishlist";
        public static readonly string ColId = "id";
        public static readonly string ColDate = "addedAt";
        public static readonly string ColItemId = "itemId";
        public static readonly string ColUserId = "uservId";


        public static readonly string ReqQuery = $@"
            SELECT {TableName}.{ColId},
                   {TableName}.{ColDate},
                   {TableName}.{ColUserId},
                   {UserSqlServer.TableName}.{UserSqlServer.ColFirstname},
                   {UserSqlServer.TableName}.{UserSqlServer.ColLastname},
                   {TableName}.{ColItemId},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColLabel}
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColPrice},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColImageItem},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColDescriptionItem}
            FROM {TableName}
            INNER JOIN {UserSqlServer.TableName} 
            ON {TableName}.{ColUserId} = {UserSqlServer.TableName}.{UserSqlServer.ColId}
            INNER JOIN {ItemSqlServer.TableName}
            ON {TableName}.{ColItemId} = {ItemSqlServer.TableName}.{ItemSqlServer.ColId}
        ";

        public static readonly string ReqGetByUserId = ReqQuery + $" WHERE {TableName}.{ColUserId} = @{ColUserId}";

        //A verifier
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName} ({ColDate}, {ColItemId}, {ColUserId}) 
            OUTPUT INSERTED.{ColId} 
            VALUES (GETDATE(), @{ColItemId}, @{ColUserId})
        ";

        public static readonly string ReqDelete = $@"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
    }
}