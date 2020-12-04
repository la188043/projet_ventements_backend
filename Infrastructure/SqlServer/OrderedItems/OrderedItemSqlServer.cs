using System.Globalization;
using Infrastructure.SqlServer.Items;
using Infrastructure.SqlServer.Orders;

namespace Infrastructure.SqlServer.OrderedItems
{
    public class OrderedItemSqlServer
    {
        public static readonly string TableName = "orderedItem";
        public static readonly string ColId = "id";
        public static readonly string ColQuantity = "quantity";
        public static readonly string ColOrderId = "ordervId";
        public static readonly string ColItemId = "itemId";

        public static readonly string ReqQuery = $@"
            SELECT {TableName}.*,
                   {OrderSqlServer.TableName}.{OrderSqlServer.ColUserId},
                   {OrderSqlServer.TableName}.{OrderSqlServer.ColIsPaid},
                   {OrderSqlServer.TableName}.{OrderSqlServer.ColOrderedAt},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColLabel},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColPrice},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColImageItem},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColDescriptionItem},
                   {ItemSqlServer.TableName}.{ItemSqlServer.ColCategoryId}
            FROM {TableName}
            INNER JOIN {OrderSqlServer.TableName}
            ON {TableName}.{ColOrderId} = {OrderSqlServer.TableName}.{OrderSqlServer.ColId}
            INNER JOIN {ItemSqlServer.TableName}
            ON {TableName}.{ColItemId} = {ItemSqlServer.TableName}.{ItemSqlServer.ColId}
        ";
        
        public static readonly string ReqGetByOrderId = ReqQuery + $" WHERE {TableName}.{ColOrderId} = @{ColOrderId}";

        public static readonly string ReqAddItemToOrder = $@"
            INSERT INTO {TableName}
            ({ColQuantity}, {ColOrderId}, {ColItemId})
            OUTPUT INSERTED.{ColId}
            VALUES (@{ColQuantity}, @{ColOrderId}, @{ColItemId})
        ";
    }
}