using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Repositories
{
    public  class InstitutionRepository: IInstitutionRepository
    {
        private readonly IDataBaseConnection _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionRepository"/> class.
        /// </summary>
        /// <param name="_db">The database connection for accessing billing data.</param>
        public InstitutionRepository(IDataBaseConnection db)
        {
            this._db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Institution>> GetInstitutionsDetails(int? id)
        {
            var spName = SPNames.SP_GETINSTITUTIONSALL; // Update the stored procedure name if necessary
            return await Task.Factory.StartNew(() => _db.Connection.Query<Institution>(spName,
                new { InstitutionID = id }, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<Institution> InsertInstitutionDetails(Institution institution)
        {
            var spName = SPNames.SP_INSERTINSTITUTION; // Name of your stored procedure
                                                   // Define parameters for the stored procedure



            var parameters = new
            {


                InstitutionName = institution.InstitutionName,
                InstitutionType = institution.InstitutionType,
                RegistrationNumber = institution.RegistrationNumber,
                Address = institution.Address,
                City = institution.City,
                State = institution.State,
                Country = institution.Country,
                ContactPerson = institution.ContactPerson,
                ContactEmail = institution.ContactEmail,
                ContactPhone = institution.ContactPhone,
                NumStudentsEligible = institution.NumStudentsEligible,
                VerificationAuthority = institution.VerificationAuthority,
                UserName = institution.Username,
                PasswordHash = institution.PasswordHash,
                RoleId = institution.RoleId,
                CreatedBy = institution.InstitutionCreatedBy



            };

            // Execute the stored procedure and retrieve the inserted data
            var insertedData = await _db.Connection.QuerySingleOrDefaultAsync<Institution>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            if (insertedData != null && !string.IsNullOrEmpty(insertedData.ErrorMessage))
            {
                throw new Exception(insertedData.ErrorMessage); // Pass it up to service/controller
            }
            return insertedData;


        }
        /// <inheritdoc/>
        public async Task UpdateInstitutionDetails(Institution institution)
        {
            var spName = SPNames.SP_UPDATEINSTITUTION; // Update the stored procedure name if necessary


            var parameters = new
            {
                InstitutionID=institution.InstitutionID,
                InstitutionName = institution.InstitutionName,
                InstitutionType = institution.InstitutionType,
                RegistrationNumber = institution.RegistrationNumber,
                Address = institution.Address,
                City = institution.City,
                State = institution.State,
                Country = institution.Country,
                ContactPerson = institution.ContactPerson,
                ContactEmail = institution.ContactEmail,
                ContactPhone = institution.ContactPhone,
                NumStudentsEligible = institution.NumStudentsEligible,
                VerificationAuthority = institution.VerificationAuthority,
                UserName = institution.Username,
                PasswordHash = institution.PasswordHash,
                RoleId = institution.RoleId,
                ModifiedBy = institution.InstitutionModifiedBy,




            };
            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure));
        }



    }
   
}
