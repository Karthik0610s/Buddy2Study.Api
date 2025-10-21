using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.DatabaseConnection;
using Buddy2Study.Infrastructure.Interfaces;
using Dapper;

namespace Buddy2Study.Infrastructure.Repositories
{
    public class ScholarshipRepository : IScholarshipRepository
    {
        private readonly IDataBaseConnection _db;

        public ScholarshipRepository(IDataBaseConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Scholarships>> GetScholarshipsDetails(int id, string role)
        {
            string spName;
            var parameters = new DynamicParameters();

            if (role?.ToLower() == "student")
            {
                spName = "sp_GetScholarshipByStudent";
                parameters.Add("@StudentId", id, DbType.Int32);
            }
            else if (role?.ToLower() == "sponsor")
            {
                spName = "sp_GetScholarshipBySponsor";
                parameters.Add("@SponsorId", id, DbType.Int32);
            }
            else
            {
                throw new ArgumentException("Role must be 'student' or 'sponsor'.");
            }

            var result = await _db.Connection.QueryAsync<Scholarships>(spName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<Scholarships> InsertScholarship(Scholarships scholarship)
        {
            var spName = "sp_InsertScholarshipDetails";
            var parameters = new DynamicParameters();

            parameters.Add("@ScholarshipCode", scholarship.ScholarshipCode);
            parameters.Add("@ScholarshipName", scholarship.ScholarshipName);
            parameters.Add("@ScholarshipType", scholarship.ScholarshipType);
            parameters.Add("@Description", scholarship.Description);
            parameters.Add("@EligibilityCriteria", scholarship.EligibilityCriteria);
            parameters.Add("@ApplicableCourses", scholarship.ApplicableCourses);
            parameters.Add("@ApplicableDepartments", scholarship.ApplicableDepartments);
            parameters.Add("@MinPercentageOrCGPA", scholarship.MinPercentageOrCGPA);
            parameters.Add("@MaxFamilyIncome", scholarship.MaxFamilyIncome);
            parameters.Add("@ScholarshipAmount", scholarship.ScholarshipAmount);
            parameters.Add("@IsRenewable", scholarship.IsRenewable);
            parameters.Add("@RenewalCriteria", scholarship.RenewalCriteria);
            parameters.Add("@StartDate", scholarship.StartDate);
            parameters.Add("@EndDate", scholarship.EndDate);
            parameters.Add("@SponsorId", scholarship.SponsorId);
            parameters.Add("@Status", scholarship.Status ?? "Active");
            parameters.Add("@ScholarshipLimit", scholarship.ScholarshipLimit);
            parameters.Add("@CreatedBy", scholarship.CreatedBy);

            return await _db.Connection.QueryFirstOrDefaultAsync<Scholarships>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<Scholarships> UpdateScholarship(Scholarships scholarship)
        {
            var spName = "sp_UpdateScholarshipDetails";
            var parameters = new DynamicParameters();

            parameters.Add("@Id", scholarship.Id);
            parameters.Add("@ScholarshipCode", scholarship.ScholarshipCode);
            parameters.Add("@ScholarshipName", scholarship.ScholarshipName);
            parameters.Add("@ScholarshipType", scholarship.ScholarshipType);
            parameters.Add("@Description", scholarship.Description);
            parameters.Add("@EligibilityCriteria", scholarship.EligibilityCriteria);
            parameters.Add("@ApplicableCourses", scholarship.ApplicableCourses);
            parameters.Add("@ApplicableDepartments", scholarship.ApplicableDepartments);
            parameters.Add("@MinPercentageOrCGPA", scholarship.MinPercentageOrCGPA);
            parameters.Add("@MaxFamilyIncome", scholarship.MaxFamilyIncome);
            parameters.Add("@ScholarshipAmount", scholarship.ScholarshipAmount);
            parameters.Add("@IsRenewable", scholarship.IsRenewable);
            parameters.Add("@RenewalCriteria", scholarship.RenewalCriteria);
            parameters.Add("@StartDate", scholarship.StartDate);
            parameters.Add("@EndDate", scholarship.EndDate);
            parameters.Add("@SponsorId", scholarship.SponsorId);
            parameters.Add("@Status", scholarship.Status ?? "Active");
            parameters.Add("@ScholarshipLimit", scholarship.ScholarshipLimit);
            parameters.Add("@ModifiedBy", scholarship.ModifiedBy);

            return await _db.Connection.QueryFirstOrDefaultAsync<Scholarships>(spName, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteScholarship(int id, string modifiedBy)
        {
            var spName = "sp_DeleteScholarship";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            parameters.Add("@ModifiedBy", modifiedBy);

            await _db.Connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
