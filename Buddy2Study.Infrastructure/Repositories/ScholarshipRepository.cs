using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Constants;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class ScholarshipRepository : IScholarshipRepository
    {
        private readonly IDataBaseConnection _db;

        public ScholarshipRepository(IDataBaseConnection db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Scholarships>> GetScholarshipsDetails(int id, string role)
        {
            string spName;
            object parameters;

            if (role?.ToLower() == "student")
            {
                spName = SPNames.SP_GETSCHOLARSHIPBYSTUDENT;
                parameters = new { StudentId = id };
            }
            else if (role?.ToLower() == "sponsor")
            {
                spName = SPNames.SP_GETSCHOLARSHIPBYSPONSOR;
                parameters = new { SponsorId = id };
            }
            else
            {
                throw new ArgumentException("Role must be 'student' or 'sponsor'.");
            }

            var result = await Task.Factory.StartNew(() =>
                _db.Connection.Query<Scholarships>(spName, parameters, commandType: CommandType.StoredProcedure).ToList()
            );

            return result;
        }

        public async Task<Scholarships> GetScholarshipById(int id)
        {
            var spName = SPNames.SP_GETSCHOLARSHIPBYID;
            var parameters = new { Id = id };

            var result = await _db.Connection.QuerySingleOrDefaultAsync<Scholarships>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        /// <inheritdoc/>
        public async Task<Scholarships> InsertScholarship(Scholarships scholarship)
        {
            var spName = SPNames.SP_INSERTSCHOLARSHIP;

            var parameters = new
            {
                ScholarshipCode = scholarship.ScholarshipCode,
                ScholarshipName = scholarship.ScholarshipName,
                ScholarshipType = string.IsNullOrEmpty(scholarship.ScholarshipType) ? null : scholarship.ScholarshipType,
                Description = string.IsNullOrEmpty(scholarship.Description) ? null : scholarship.Description,
                EligibilityCriteria = string.IsNullOrEmpty(scholarship.EligibilityCriteria) ? null : scholarship.EligibilityCriteria,
                ApplicableCourses = string.IsNullOrEmpty(scholarship.ApplicableCourses) ? null : scholarship.ApplicableCourses,
                ApplicableDepartments = string.IsNullOrEmpty(scholarship.ApplicableDepartments) ? null : scholarship.ApplicableDepartments,
                MinPercentageOrCGPA = scholarship.MinPercentageOrCGPA,
                MaxFamilyIncome = scholarship.MaxFamilyIncome,
                ScholarshipAmount = scholarship.Benefits,
                IsRenewable = scholarship.IsRenewable,
                RenewalCriteria = string.IsNullOrEmpty(scholarship.RenewalCriteria) ? null : scholarship.RenewalCriteria,
                StartDate = scholarship.StartDate,
                EndDate = scholarship.EndDate,
                SponsorId = scholarship.SponsorId,
                Status = scholarship.Status ?? "Active",
                ScholarshipLimit = scholarship.ScholarshipLimit,
                CreatedBy = scholarship.CreatedBy,
                Documents = scholarship.Documents,
                Eligibility = scholarship.Eligibility,
                WebportaltoApply = scholarship.WebportaltoApply,
                CanApply = scholarship.CanApply,
                ContactDetails = scholarship.ContactDetails
            };

            var insertedData = await _db.Connection.QuerySingleOrDefaultAsync<Scholarships>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return insertedData;
        }

        /// <inheritdoc/>
        public async Task<Scholarships> UpdateScholarship(Scholarships scholarship)
        {
            var spName = SPNames.SP_UPDATESCHOLARSHIP;

            var parameters = new
            {
                Id = scholarship.Id,
                ScholarshipCode = scholarship.ScholarshipCode,
                ScholarshipName = scholarship.ScholarshipName,
                ScholarshipType = string.IsNullOrEmpty(scholarship.ScholarshipType) ? null : scholarship.ScholarshipType,
                Description = string.IsNullOrEmpty(scholarship.Description) ? null : scholarship.Description,
                EligibilityCriteria = string.IsNullOrEmpty(scholarship.EligibilityCriteria) ? null : scholarship.EligibilityCriteria,
                ApplicableCourses = string.IsNullOrEmpty(scholarship.ApplicableCourses) ? null : scholarship.ApplicableCourses,
                ApplicableDepartments = string.IsNullOrEmpty(scholarship.ApplicableDepartments) ? null : scholarship.ApplicableDepartments,
                MinPercentageOrCGPA = scholarship.MinPercentageOrCGPA,
                MaxFamilyIncome = scholarship.MaxFamilyIncome,
                ScholarshipAmount = scholarship.Benefits,
                IsRenewable = scholarship.IsRenewable,
                RenewalCriteria = string.IsNullOrEmpty(scholarship.RenewalCriteria) ? null : scholarship.RenewalCriteria,
                StartDate = scholarship.StartDate,
                EndDate = scholarship.EndDate,
                SponsorId = scholarship.SponsorId,
                Status = scholarship.Status ?? "Active",
                ScholarshipLimit = scholarship.ScholarshipLimit,
                ModifiedBy = scholarship.ModifiedBy,
                Documents = scholarship.Documents,
                Eligibility = scholarship.Eligibility,
                WebportaltoApply = scholarship.WebportaltoApply,
                CanApply = scholarship.CanApply,
                ContactDetails = scholarship.ContactDetails
            };

            await _db.Connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);

            return scholarship;
        }

        /// <inheritdoc/>
        public async Task DeleteScholarship(int id, string modifiedBy)
        {
            var spName = SPNames.SP_DELETESCHOLARSHIP;

            var parameters = new
            {
                Id = id,
                ModifiedBy = modifiedBy
            };

            await Task.Factory.StartNew(() =>
                _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure)
            );
        }
        public async Task<IEnumerable<ScholarshipStatus>> GetScholarshipsByStatus(string statusType)
        {
            var spName = SPNames.SP_GETSCHOLARSHIPSBYSTATUS; // SP name
            var parameters = new { StatusType = statusType };

            var result = await _db.Connection.QueryAsync<ScholarshipStatus>(
                spName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<FeaturedScholarship>> GetFeaturedScholarships()
        {
            var spName = SPNames.SP_GETFEATUREDSCHOLARSHIPS;

            var result = await _db.Connection.QueryAsync<FeaturedScholarship>(
                spName,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

    }
}
