namespace Infrastructure.SqlServer.Categories
{
    public class CategorySqlServer
    {
        public static readonly string TableName = "category";
        public static readonly string ColId = "id";
        public static readonly string ColTitle = "title";
        
        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGetById = ReqQuery + $" WHERE {ColId} = @{ColId}";
        
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColTitle})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColTitle})
        ";
        
        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColTitle} = @{ColTitle}
            WHERE {ColId} = @{ColId}
        ";
        
    }
}