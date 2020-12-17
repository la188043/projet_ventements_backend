using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application.Exceptions;
using Application.Repositories;
using Domain.Addresses;
using Domain.Users;
using Infrastructure.SqlServer.Factories;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.Users
{
    public class UserRepository : IUserRepository
    {
        private IInstanceFromReader<IUser> _factoryUser = new UserFactory();
        private IInstanceFromReader<IAddress> _factoryAddress = new AddressFactory();
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
                    users.Add(_factoryUser.CreateFromReader(reader));
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
                    return _factoryUser.CreateFromReader(reader);
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

                try
                {
                    user.Id = (int) cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                    throw new DuplicateSqlPrimaryException("Cet email est déjà utilisé");
                }
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
                    return _factoryUser.CreateFromReader(reader);
            }

            return null;
        }

        public bool Delete(int userId)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqDelete;

                cmd.Parameters.AddWithValue($"@{UserSqlServer.ColId}", userId);

                return cmd.ExecuteNonQuery() > 0;
            }
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

        public IAddress GetUserAddress(int idUser)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserSqlServer.ReqGetUserAddress;

                cmd.Parameters.AddWithValue($@"@{UserSqlServer.ColId}", idUser);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                    return _factoryAddress.CreateFromReader(reader);
            }

            return null;
        }
    }
}