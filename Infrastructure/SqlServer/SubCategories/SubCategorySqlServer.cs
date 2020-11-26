using Infrastructure.SqlServer.Categories;

namespace Infrastructure.SqlServer.SubCategories
{
    public class SubCategorySqlServer
    {
        public static readonly string TableName = "subcategory";
        public static readonly string ColId = "id";
        public static readonly string ColTitle = "title";
        public static readonly string ColIdCategory = "categoryId";
        
        // public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqQuery = $@"
            SELECT * FROM {TableName}
            INNER JOIN {CategorySqlServer.TableName}
            ON {TableName}.{ColIdCategory} = {CategorySqlServer.TableName}.{CategorySqlServer.ColId}
        ";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        public static readonly string ReqAdd = $@"
            INSERT INTO {TableName} ({ColTitle}, {ColIdCategory}) 
            OUTPUT Inserted.{ColId} 
            VALUES (@{ColTitle}, @{ColIdCategory})
        ";
        
        /*
        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColTitle} = @{ColTitle},
            {ColIdCategory} = @{ColIdCategory}
            WHERE {ColId} = @{ColId}
        ";
        */

        public static readonly string ReqGetByCategoryId =
            ReqQuery + $" WHERE {TableName}.{ColIdCategory} = @{ColIdCategory}";
   }
}