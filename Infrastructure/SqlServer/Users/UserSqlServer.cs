using Infrastructure.SqlServer.Addresses;

namespace Infrastructure.SqlServer.Users
{
    public class UserSqlServer
    {
        public static readonly string TableName = "userv";
        public static readonly string ColId = "id";
        public static readonly string ColFirstname = "firstname";
        public static readonly string ColLastname = "lastname";
        public static readonly string ColBirthDate = "birthdate";
        public static readonly string ColEmail = "email";
        public static readonly string ColPassword = "encryptedPassword";
        public static readonly string ColGender = "gender";
        public static readonly string ColAdmin = "administrator";
        public static readonly string ColAddress = "addressId";

        public static readonly string ReqQuery = $@"
            SELECT {TableName}.{ColId},
                   {TableName}.{ColFirstname},
                   {TableName}.{ColLastname},
                   {TableName}.{ColBirthDate},
                   {TableName}.{ColEmail},
                   {TableName}.{ColPassword},
                   {TableName}.{ColGender},
                   {TableName}.{ColAdmin},
                   {TableName}.{ColAddress},
                   {AddressSqlServer.TableName}.{AddressSqlServer.ColStreet},
                   {AddressSqlServer.TableName}.{AddressSqlServer.ColHomeNumber},
                   {AddressSqlServer.TableName}.{AddressSqlServer.ColZip},
                   {AddressSqlServer.TableName}.{AddressSqlServer.ColCity}
            FROM {TableName}
            LEFT JOIN {AddressSqlServer.TableName}
            ON {TableName}.{ColAddress} = {AddressSqlServer.TableName}.{AddressSqlServer.ColId}
        ";

        public static readonly string ReqGetById = ReqQuery + $" WHERE {TableName}.{ColId} = @{ColId}";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName} 
            ({ColFirstname}, {ColLastname}, {ColBirthDate}, {ColEmail}, {ColPassword}, {ColGender})
            OUTPUT INSERTED.{ColId}
            VALUES
            (@{ColFirstname}, @{ColLastname}, @{ColBirthDate}, @{ColEmail}, @{ColPassword}, @{ColGender})
        ";

        public static readonly string ReqAuthenticate = ReqQuery + $" WHERE {ColEmail} = @{ColEmail}";

        public static readonly string ReqAddAddressId = $@"
            UPDATE {TableName} SET {ColAddress} = @{ColAddress}
            WHERE {ColId} = @{ColId}
        ";

        public static readonly string ReqGetUserAddress = $@"
            SELECT * FROM {AddressSqlServer.TableName}
            WHERE {AddressSqlServer.TableName}.{AddressSqlServer.ColId} IN
            (
                SELECT {TableName}.{ColAddress} FROM {TableName}
                WHERE {TableName}.{ColId} = @{ColId}
            )
        ";
    }
}