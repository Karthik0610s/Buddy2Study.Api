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
    public class SponsorRepository : ISponsorRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="SponsorRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public SponsorRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Sponsors>> GetSponsorsDetails(int? id)
        {
            var spName = SPNames.SP_GETSPONSORSALL; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Sponsors>(spName,
                new { Id = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Sponsors> InsertSponsorDetails(Sponsors Sponsors)
        {
            var spName = SPNames.SP_INSERTSPONSOR; // Name of your stored procedure
                                                   // Define parameters for the stored procedure



            var parameters = new
            {


                Sponsors.OrganizationName,
                Sponsors.OrganizationType,     // Again, verify naming
                Sponsors.Email,
                Sponsors.Phone,
                Sponsors.Website,
                Sponsors.Username,
                Sponsors.PasswordHash,
                Sponsors.RoleId,
                Sponsors.CreatedBy,



            };

            // Execute the stored procedure and retrieve the inserted data
            var insertedData = await _db.Connection.QueryFirstOrDefaultAsync<Sponsors>(
                            spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return insertedData;


        }
        /// <inheritdoc/>
        public async Task<Sponsors> UpdateSponsorDetails(Sponsors sponsor)
        {
            var spName = SPNames.SP_UPDATESPONSOR;

            var parameters = new
            {
                SponsorId = sponsor.Id,
                OrganizationName = sponsor.OrganizationName,
                OrganizationType = sponsor.OrganizationType,
                Email = sponsor.Email,
                Phone = sponsor.Phone,
                Website = sponsor.Website,
                ContactPerson = sponsor.ContactPerson,
                Address = sponsor.Address,
                Budget = sponsor.Budget,
                StudentCriteria = sponsor.StudentCriteria,
                StudyLevels = sponsor.StudyLevels,
                ModifiedBy = sponsor.ModifiedBy
            };

            // Execute SP and return updated sponsor
            var updatedSponsor = await _db.Connection.QueryFirstOrDefaultAsync<Sponsors>(
                spName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
            );

            return updatedSponsor;
        }


    }
}