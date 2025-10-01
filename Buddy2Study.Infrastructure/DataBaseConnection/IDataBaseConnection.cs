using System.Data;

namespace Buddy2Study.Infrastructure.DatabaseConnection
{
    public interface IDataBaseConnection
    {
        IDbConnection Connection { get; }
    }
}
