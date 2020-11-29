using System.Globalization;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.BaggedItems
{
    public class BaggedItemSqlServer
    {
        public static readonly string TableName = "baggedItem";
        public static readonly string ColId = "id";
        public static readonly string ColAddedAt = "addedAt";
        public static readonly string ColQuantity = "quantity";
        public static readonly string ColSize = "size";
        public static readonly string ColUserId = "uservId";
        public static readonly string ColItemId = "itemId";

        public static readonly string ReqQueryJoinUsersAndItems = $@"
            SELECT {TableName}.*,
                   {UserSqlServer.TableName}.{UserSqlServer.ColFirstname},
                   {UserSqlServer.TableName}.{UserSqlServer.ColLastname},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColLabel},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColPrice},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColImageItem},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColDescriptionItem}
            FROM {TableName}
            INNER JOIN {UserSqlServer.TableName}
            ON {TableName}.{ColUserId} = {UserSqlServer.TableName}.{UserSqlServer.ColId}
            INNER JOIN {ItemSqlServer.TableName}
            ON {TableName}.{ColItemId} = {ItemSqlServer.TableName}.{ItemSqlServer.ColId}
            WHERE {TableName}.{ColUserId} = @{ColUserId}
        ";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}
            ({ColAddedAt}, {ColQuantity}, {ColSize}, {ColUserId}, {ColItemId})
            OUTPUT INSERTED.{ColId}
            VALUES
            (GETDATE(), @{ColQuantity}, @{ColSize}, @{ColUserId}, @{ColItemId})
        ";

        public static readonly string ReqEmptyBag = $"DELETE FROM {TableName} WHERE {ColUserId} = @{ColUserId}";

        public static readonly string ReqDeleteItem = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";

        public static readonly string ReqUpdateQuantity = 
            $"UPDATE {TableName} SET {ColQuantity} = @{ColQuantity} WHERE {ColId} = @{ColId}";
    }
}