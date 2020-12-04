using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Orders
{
    public class OrderSqlServer
    {
        public static readonly string TableName = "orderv";
        public static readonly string ColId = "id";
        public static readonly string ColIsPaid = "isPaid";
        public static readonly string ColOrderedAt = "orderedAt";
        public static readonly string ColUserId = "uservId";

        public static readonly string ReqQuery = $@"
            SELECT {TableName}.*,
                   {UserSqlServer.TableName}.{UserSqlServer.ColFirstname},
                   {UserSqlServer.TableName}.{UserSqlServer.ColLastname},
                   {UserSqlServer.TableName}.{UserSqlServer.ColEmail},
            FROM {TableName}
            INNER JOIN {UserSqlServer.TableName}
            ON {TableName}.{ColUserId} = {UserSqlServer.TableName}.{ColId}
        ";

        public static readonly string ReqGetByUserId = ReqQuery + $" WHERE {TableName}.{ColUserId} = @{ColUserId}";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}
            ({ColOrderedAt}, {ColUserId})
            OUTPUT INSERTED.{ColId}
            VALUES (GETDATE(), @{ColUserId});
        ";
    }
}