namespace Infrastructure.SqlServer.Categories
{
    public class CategorySqlServer
    {
        public static readonly string TableName = "category";
        public static readonly string TableAliasParentCategory = "parentCategory";
        public static readonly string TableAliasChildCategory = "childCategory";
        public static readonly string ColId = "id";
        public static readonly string ColTitle = "title";
        public static readonly string ColCategoryId = "categoryId";

        public static readonly string ColAliasParentId = "parentId";
        public static readonly string ColAliasParentTitle = "parentTitle";
        public static readonly string ColAliasChildId = "childId";
        public static readonly string ColAliasChildTitle = "childTitle";

        public static readonly string ReqQuery = $@"
            SELECT {TableAliasParentCategory}.{ColId} AS {ColAliasParentId},
                   {TableAliasParentCategory}.{ColTitle} AS {ColAliasParentTitle},
                   {TableAliasChildCategory}.{ColId} AS {ColAliasChildId},
                   {TableAliasChildCategory}.{ColTitle} AS {ColAliasChildTitle}
            FROM {TableName} {TableAliasChildCategory}
            LEFT JOIN {TableName} {TableAliasParentCategory}
            ON {TableAliasChildCategory}.{ColCategoryId} = {TableAliasParentCategory}.{ColId}
        ";

        public static readonly string ReqGetById =
            ReqQuery + $" WHERE {TableAliasParentCategory}.{ColId} = @{ColId}";

        public static readonly string ReqCreateCategory = $@"
            INSERT INTO {TableName} ({ColTitle})
            OUTPUT INSERTED.{ColId}
            VALUES (@{ColTitle})
        ";

        public static readonly string ReqCreateSubCategory = $@"
            INSERT INTO {TableName} ({ColTitle}, {ColCategoryId})
            OUTPUT INSERTED.{ColId}
            VALUES (@{ColTitle}, @{ColCategoryId})
        ";
    }
}