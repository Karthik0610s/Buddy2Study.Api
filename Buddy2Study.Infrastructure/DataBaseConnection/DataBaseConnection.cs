using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using Buddy2Study.Infrastructure.DataBaseConnection;
using Microsoft.Extensions.Options;

namespace Buddy2Study.Infrastructure.DatabaseConnection
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
