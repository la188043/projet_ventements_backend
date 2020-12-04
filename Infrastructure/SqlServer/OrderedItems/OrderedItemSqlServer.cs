namespace Infrastructure.SqlServer.OrderedItems
{
    public class OrderedItemSqlServer
    {
        public static readonly string TableName = "orderedItem";
        public static readonly string ColId = "id";
        public static readonly string ColOrderId = "ordervId";
        public static readonly string ColItemId = "itemId";

        public static readonly string ReqQuery = $@"";
        
        public static readonly string ReqGetByOrderId = $@"";
    }
}