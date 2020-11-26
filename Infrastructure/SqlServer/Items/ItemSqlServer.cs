using Infrastructure.SqlServer.SubCategories;

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
        
        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {ColId} = @{ColId}";

        
        public static readonly string ReqAdd = $@"INSERT INTO {TableName} ({ColLabel}, {ColPrice}, {ColQuantity}, {ColImageItem}, {ColDescriptionItem}, {ColSize}, {ColSubCategoryId}) OUTPUT 
        Inserted.{ColId} VALUES (@{ColLabel}, @{ColPrice}, @{ColQuantity}, @{ColImageItem}, @{ColDescriptionItem}, @{ColSize}, @{ColSubCategoryId})";
        
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
        
        public static readonly string ReqGetBySubCategoryId = $@"
            SELECT * FROM {TableName}
            WHERE {ColSubCategoryId} = @{ColSubCategoryId}
        ";
    }
    
}