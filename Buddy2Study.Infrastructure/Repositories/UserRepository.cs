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
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Users>> GetUsersDetails(int? id)
        {
            var spName = SPNames.SP_GETUSERSALL; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Users>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Users> InsertUserDetails(Users users)
        {
            var spName = SPNames.SP_INSERTSTUDENT; // Name of your stored procedure


            var parameters = new
            {

                users.FirstName,
                users.LastName,      
                users.Email,
                users.Phone,
                users.DateofBirth,
                users.UserName,
                users.PasswordHash,
                users.Gender,
                users.Education,
                users.RoleId,
                users.CreatedBy,



            };

            // Execute the stored procedure and retrieve the inserted data
            var insertedData = await _db.Connection.QuerySingleOrDefaultAsync<Users>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return insertedData;


        }
        /// <inheritdoc/>
        public async Task UpdateUserDetails(Users users)
        {
            var spName = SPNames.SP_UPDATESTUDENT; // Update the stored procedure name if necessary


            var parameters = new
            {
                users.Id,
                users.FirstName,
                users.LastName,     // Again, verify naming
                users.Email,
                users.Phone,
                users.DateofBirth,
                users.UserName,
                users.PasswordHash,
                users.Gender,
                users.Education,
                users.RoleId,
                users.ModifiedBy,


            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

    }
}
