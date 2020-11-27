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
        public static readonly string ColSubCategoryId = "subcategoryId";
        
        public static readonly string ReqQuery = $@"
            SELECT * FROM {TableName}
            INNER JOIN {CategorySqlServer.TableName} 
            ON {TableName}.{ColSubCategoryId} = {CategorySqlServer.TableName}.{CategorySqlServer.ColId}
        ";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        
        public static readonly string ReqAdd = $@"
            INSERT INTO {TableName} 
            ({ColLabel}, {ColPrice}, {ColQuantity}, {ColImageItem}, {ColDescriptionItem}, {ColSize}, {ColSubCategoryId}) 
            OUTPUT INSERTED.{ColId} 
            VALUES 
            (@{ColLabel}, @{ColPrice}, @{ColQuantity}, @{ColImageItem}, @{ColDescriptionItem}, @{ColSize}, @{ColSubCategoryId})
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
            {ColSubCategoryId} = @{ColSubCategoryId}
            WHERE {ColId} = @{ColId}
        ";
        */
        
        public static readonly string ReqGetBySubCategoryId = ReqQuery + $" WHERE {TableName}.{ColSubCategoryId} = @{ColSubCategoryId}";
    }
    
}