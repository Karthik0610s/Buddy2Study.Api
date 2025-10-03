using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class ScholarshipApplicationFormRepository : IScholarshipApplicationFormRepository
    {
        


        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScholarshipApplicationFormRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public ScholarshipApplicationFormRepository(IDataBaseConnection db)
        {
            this._db = db;
        }
        public async Task<IEnumerable<ScholarshipApplicationForm>> GetScholarshipsApplicationForm(int? id)
        {
            var spName = SPNames.SP_GETALLSCHOLARSHIPAPPLICATIONFORM; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<ScholarshipApplicationForm>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }


        public async Task<ScholarshipApplicationForm> InsertScholarshipApplicationForm(ScholarshipApplicationForm scholarship)
        {
            var spName = SPNames.SP_INSERTSCHOLARSHIPAPPLICATIONFORM; // Stored procedure name

            var parameters = new
            {
                // Personal Information
                FirstName = scholarship.FirstName,
                LastName = scholarship.LastName,
                Email = scholarship.Email,
                PhoneNumber = scholarship.PhoneNumber,
                DateOfBirth = scholarship.DateOfBirth,
                Gender = scholarship.Gender,

                // Academic Information
                StudyLevel = scholarship.StudyLevel,
                SchoolName = scholarship.SchoolName,
                CourseOrMajor = scholarship.CourseOrMajor,
                YearOfStudy = scholarship.YearOfStudy,
                GPAOrMarks = scholarship.GPAOrMarks,

                // Scholarship Details
                ScholarshipId = scholarship.ScholarshipId,
                Category = scholarship.Category,
                ApplicationDate = scholarship.ApplicationDate,
                FilePath = scholarship.FilePath,
                FileName = scholarship.FileName,

                // Additional Information
                ExtraCurricularActivities = scholarship.ExtraCurricularActivities,
                AwardsAchievements = scholarship.AwardsAchievements,
                NotesComments = scholarship.NotesComments,

                // Status & Audit
                Status = scholarship.Status,
                CreatedBy = scholarship.CreatedBy
            };

            // Execute stored procedure and map to DTO
            var insertedScholarship = await _db.Connection.QueryFirstOrDefaultAsync<ScholarshipApplicationForm>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return insertedScholarship;
        }

        public async Task UpdateScholarshipApplicationForm(ScholarshipApplicationForm scholarship)
        {
            var spName = SPNames.SP_UPDATESCHOLARSHIPAPPLICATIONFORM; // Stored procedure name

            var parameters = new
            {
                 Id=scholarship.Id,
                // Personal Information
                FirstName = scholarship.FirstName,
                LastName = scholarship.LastName,
                Email = scholarship.Email,
                PhoneNumber = scholarship.PhoneNumber,
                DateOfBirth = scholarship.DateOfBirth,
                Gender = scholarship.Gender,

                // Academic Information
                StudyLevel = scholarship.StudyLevel,
                SchoolName = scholarship.SchoolName,
                CourseOrMajor = scholarship.CourseOrMajor,
                YearOfStudy = scholarship.YearOfStudy,
                GPAOrMarks = scholarship.GPAOrMarks,

                // Scholarship Details
                ScholarshipId = scholarship.ScholarshipId,
                Category = scholarship.Category,
                ApplicationDate = scholarship.ApplicationDate,
                FilePath = scholarship.FilePath,
                FileName = scholarship.FileName,

                // Additional Information
                ExtraCurricularActivities = scholarship.ExtraCurricularActivities,
                AwardsAchievements = scholarship.AwardsAchievements,
                NotesComments = scholarship.NotesComments,

                // Status & Audit
                Status = scholarship.Status,
                ModifiedBy = scholarship.ModifiedBy
            };

            // Execute stored procedure and map to DTO

            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

        public async Task<bool> DeleteScholarshipApplicationForm(int id)
        {
            var spName = SPNames.SP_DELETESCHOLARSHIPAPPLICATIONFORM; // Update the stored procedure name if necessary
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure));
            return true;
        }

        public Task<string> UpdateFilepathdata(string target, int id, string filesList, string TypeofFile)
        {
            var spName = SPNames.SP_UPDATEFILE;
            //  var list = new List<string>;

            return Task.Factory.StartNew(() => _db.Connection.Query<string>(spName,
                new { Id = id, filepath = target, files = filesList, typeofFile = TypeofFile },
                commandType: CommandType.StoredProcedure).ToString());
        }
    }
}
