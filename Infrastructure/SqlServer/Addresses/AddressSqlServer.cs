namespace Infrastructure.SqlServer.Addresses
{
    public class AddressSqlServer
    {
        public static readonly string TableName = "addressuserv";
        public static readonly string ColId = "id";
        public static readonly string ColStreet = "street";
        public static readonly string ColHomeNumber = "homeNumber";
        public static readonly string ColZip = "zip";
        public static readonly string ColCity = "city";

        public static readonly string ReqGetById = $"SELECT * FROM {TableName} WHERE {ColId} = @{ColId}";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName} ({ColStreet}, {ColHomeNumber}, {ColZip}, {ColCity})
            OUTPUT INSERTED.{ColId}
            VALUES (@{ColStreet}, @{ColHomeNumber}, @{ColZip}, @{ColCity})
        ";

        public static readonly string ReqCheck = $@"
            SELECT * FROM {TableName}
            WHERE {ColStreet} LIKE @{ColStreet} 
            AND {ColHomeNumber} = @{ColHomeNumber}
            AND {ColZip} LIKE @{ColZip}
            AND {ColCity} LIKE @{ColCity}
        ";
    }
}