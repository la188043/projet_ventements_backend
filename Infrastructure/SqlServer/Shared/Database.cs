using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Shared
{
    public class Database
    {
        private readonly static string ConnectionString =
            @"Server=LORENZUS\SQLEXPRESS;Database=sql_ventements_project;Integrated Security=SSPI";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}