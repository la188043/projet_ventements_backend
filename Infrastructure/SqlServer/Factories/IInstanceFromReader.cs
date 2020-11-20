using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Factories
{
    public interface IInstanceFromReader<out T>
    {
        T CreateFromReader(SqlDataReader reader);
    }
}