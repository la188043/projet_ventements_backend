using System.Data.SqlClient;
using Domain.Users;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factories
{
    public class UserFactory : IInstanceFromReader<IUser>
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal(UserSqlServer.ColId)),
                Firstname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColFirstname)),
                Lastname = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastname)),
                Birthdate = reader.GetDateTime(reader.GetOrdinal(UserSqlServer.ColBirthDate)),
                Email = reader.GetString(reader.GetOrdinal(UserSqlServer.ColEmail)),
                EncryptedPassword = reader.GetString(reader.GetOrdinal(UserSqlServer.ColPassword)),
                Gender = reader.GetString(reader.GetOrdinal(UserSqlServer.ColGender))[0],
                Administrator = reader.GetBoolean(reader.GetOrdinal(UserSqlServer.ColAdmin)),
            };
        }
    }
}