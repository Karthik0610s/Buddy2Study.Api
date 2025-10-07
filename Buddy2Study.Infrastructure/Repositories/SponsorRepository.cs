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
        public async Task UpdateSponsorDetails(Sponsors Sponsors)
        {
            var spName = SPNames.SP_UPDATESPONSOR; // Update the stored procedure name if necessary


            var parameters = new
            {
                Sponsors.Id,
                Sponsors.OrganizationName,
                Sponsors.OrganizationType,     // Again, verify naming
                Sponsors.Email,
                Sponsors.Phone,
                Sponsors.Website,
                Sponsors.Username,
                Sponsors.PasswordHash,
                Sponsors.RoleId,
                Sponsors.ModifiedBy,

                


            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }

       

    }
}
