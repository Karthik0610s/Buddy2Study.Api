using System.Data;
using Dapper;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDataBaseConnection _db;

        public StudentRepository(IDataBaseConnection db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Students>> GetStudentsDetails(int? id)
        {
            // ✅ Correct SP for Students
            var spName = SPNames.SP_GETALLSTUDENT;

            return await Task.Factory.StartNew(() =>
                _db.Connection.Query<Students>(
                    spName,
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).ToList());
        }

        /// <inheritdoc/>
        public async Task<Students> InsertStudentDetails(Students student)
        {
            var spName = SPNames.SP_INSERTSTUDENT;

            var parameters = new
            {
                student.FirstName,
                student.LastName,
                student.Email,
                student.Phone,
                student.DateofBirth,
                student.Gender,
                student.UserName,
                student.PasswordHash,
                student.Education,
                student.RoleId,

                student.CreatedBy
            };

            // Execute the stored procedure and retrieve the inserted student
            var insertedData = await _db.Connection.QuerySingleOrDefaultAsync<Students>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return insertedData;
        }

        /// <inheritdoc/>
        public async Task UpdateStudentDetails(Students student)
        {
            var spName = SPNames.SP_UPDATESTUDENT;

            var parameters = new
            {
                student.Id,
                student.FirstName,
                student.LastName,
                student.Email,
                student.Phone,
                student.DateofBirth,
                student.Gender,
                student.UserName,
                student.PasswordHash,
                student.Education,
                student.RoleId,
                student.ModifiedBy
            };

            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }
    }
}
