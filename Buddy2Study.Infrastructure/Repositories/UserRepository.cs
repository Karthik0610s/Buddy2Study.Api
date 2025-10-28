using System.Data;
using Dapper;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBaseConnection _db;

        public UserRepository(IDataBaseConnection db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Users>> GetUsersDetails(int? id)
        {
            var spName = SPNames.SP_GETUSERSALL; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() =>
                _db.Connection.Query<Users>(spName,
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).ToList()
            );
        }

        /// <inheritdoc/>
        public async Task<Users?> GetByEmailAsync(string email)
        {
            var spName = SPNames.SP_GETUSERBYEMAIL; // ✅ Create this stored procedure constant in SPNames.cs
            return await Task.Factory.StartNew(() =>
                _db.Connection.QueryFirstOrDefault<Users>(
                    spName,
                    new { Email = email },
                    commandType: CommandType.StoredProcedure
                )
            );
        }
    }
}
