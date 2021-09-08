using System.Data.Common;
using System.Data.SqlClient;

namespace VendasOnLine.Infra
{
    public class SqlDatabase: IDatabase
    {
        public DbConnection CreateConnection()
        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=VendasOnLine;Integrated Security=True");
        }
    }
}
