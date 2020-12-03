namespace Infrastructure.SqlServer.Orders
{
    public class OrderSqlServer
    {
        public static readonly string TableName = "orderv";
        public static readonly string ColId = "id";
        public static readonly string ColIsPaid = "isPaid";
        public static readonly string ColOrderedAt = "orderedAt";
        public static readonly string ColUserId = "uservId";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}
            ({ColOrderedAt}, {ColUserId})
            OUTPUT INSERTED.{ColId}
            VALUES (GETDATE(), @{ColUserId});
        ";
    }
}