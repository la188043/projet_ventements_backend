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
    }
}