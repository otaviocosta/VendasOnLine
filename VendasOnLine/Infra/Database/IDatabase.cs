using System.Data.Common;

namespace VendasOnLine.Infra
{
    public interface IDatabase
    {
        DbConnection CreateConnection();
    }
}
