using Infrastructure.SqlServer.Categories;

namespace Infrastructure.SqlServer.Items
{
    public class ItemSqlServer
    {
        public static readonly string TableName = "item";
        public static readonly string ColId = "id";
        public static readonly string ColLabel = "label";
        public static readonly string ColPrice = "price";
        public static readonly string ColQuantity = "quantity";
        public static readonly string ColImageItem = "imageItem";
        public static readonly string ColDescriptionItem = "descriptionItem";
        public static readonly string ColSize = "size";
        public static readonly string ColCategoryId = "categoryId";
        
        public static readonly string ReqQuery = $@"
            SELECT {TableName}.{ColId},
                   {TableName}.{ColLabel},
                   {TableName}.{ColPrice},
                   {TableName}.{ColQuantity},
                   {TableName}.{ColImageItem},
                   {TableName}.{ColDescriptionItem},
                   {TableName}.{ColSize},
                   {TableName}.{ColCategoryId},
                   {CategorySqlServer.TableName}.{CategorySqlServer.ColTitle},
            FROM {TableName}
            INNER JOIN {CategorySqlServer.TableName} 
            ON {TableName}.{ColCategoryId} = {CategorySqlServer.TableName}.{CategorySqlServer.ColId}
        ";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        
        public static readonly string ReqAdd = $@"
            INSERT INTO {TableName} 
            ({ColLabel}, {ColPrice}, {ColQuantity}, {ColImageItem}, {ColDescriptionItem}, {ColSize}, {ColCategoryId}) 
            OUTPUT INSERTED.{ColId} 
            VALUES 
            (@{ColLabel}, @{ColPrice}, @{ColQuantity}, @{ColImageItem}, @{ColDescriptionItem}, @{ColSize}, @{ColCategoryId})
        ";
        
        /*
        public static readonly string ReqPut = $@"
            UPDATE {TableName} SET
            {ColLabel} = @{ColLabel},
            {ColPrice} = @{ColPrice},
            {ColQuantity} = @{ColQuantity},
            {ColImageItem} = @{ColImageItem},
            {ColDescriptionItem} = @{ColDescriptionItem},
            {ColSize} = @{ColSize},
            {ColCategoryId} = @{ColCategoryId}
            WHERE {ColId} = @{ColId}
        ";
        */
        
        public static readonly string ReqGetBySubCategoryId = ReqQuery + $" WHERE {TableName}.{ColCategoryId} = @{ColCategoryId}";
    }
    
}