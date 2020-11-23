using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Addresses;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Users
{
    public class UserRepository : IUserRepository
    {
        private IInstanceFromReader<IUser> _factory = new UserFactory();
        public IEnumerable<IUser> Query()
        {
            IList<IUser> users = new List<IUser>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqQuery;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                
                while (reader.Read())
                    users.Add(_factory.CreateFromReader(reader));
            }

            return users;
        }

        public IUser GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqGetById;

                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColId}", id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
        }

        public IUser Create(IUser user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqCreate;

                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColFirstname}", user.Firstname);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColLastname}", user.Lastname);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColBirthDate}", user.Birthdate);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColEmail}", user.Email);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColPassword}", user.EncryptedPassword);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColGender}", user.Gender);

                user.Id = (int) cmd.ExecuteScalar();
            }

            return user;
        }

        public IUser Authenticate(IUser user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqAuthenticate;

                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColEmail}", user.Email);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factory.CreateFromReader(reader);
            }

            return null;
        }

        public bool RegisterAddress(int idUser, IAddress address)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqAddAddressId;

                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColId}", idUser);
                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColAddress}", address.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}