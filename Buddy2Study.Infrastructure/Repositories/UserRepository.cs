using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Buddy2Study.Infrastructure.DatabaseConnection;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
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
            var spName = SPNames.SP_INSERTUSER; // Name of your stored procedure
                                                // Define parameters for the stored procedure



            var parameters = new
            {

                users.FirstName,
                users.LatName,      // Note: double-check if this should be "LastName"
                users.Email,
                users.Phone,
                users.DateofBirth,
                users.UserName,
                users.Password,
                users.Gender,
                users.Course,
                users.College,
                users.Year,
                users.CreatedBy,
                users.CreatedDate


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
            var spName = SPNames.SP_UPDATEUSER; // Update the stored procedure name if necessary


            var parameters = new
            {
                users.Id,
                users.FirstName,
                users.LatName,     // Again, verify naming
                users.Email,
                users.Phone,
                users.DateofBirth,
                users.UserName,
                users.Password,
                users.Gender,
                users.Course,
                users.College,
                users.Year,
                users.ModifiedBy,
                users.ModifiedDate

            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteUserDetails(int id)
        {
            var spName = SPNames.SP_DELETEUSER; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }


    }
}
