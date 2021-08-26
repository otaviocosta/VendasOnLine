using System.Data.SqlClient;

namespace VendasOnLine.Infra
{
    public abstract class Database
    {
        protected SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
