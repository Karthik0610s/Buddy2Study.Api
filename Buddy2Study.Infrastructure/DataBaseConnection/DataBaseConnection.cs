using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;


namespace Buddy2Study.Infrastructure.DataBaseConnection
{
    public sealed class DataBaseConnection : IDataBaseConnection
    {
        public DataBaseConnection(IOptions<ConnectionStrings> connectionStrings)
        {
            Connection = new SqlConnection(connectionStrings.Value.DbConnection);
        }

        public IDbConnection Connection { get; }
    }
}
