using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{
    public interface IScholarshipService
    {
        Task<IEnumerable<ScholarshipDto>> GetScholarshipsDetails(int id, string role);
        Task<ScholarshipDto?> GetScholarshipById(int id);

        Task<ScholarshipDto> InsertScholarship(ScholarshipDto scholarship);
        Task<ScholarshipDto> UpdateScholarship(ScholarshipDto scholarship);
        Task<bool> DeleteScholarship(int id, string modifiedBy);

        /// <summary>
        /// Get scholarships by status (live or upcoming)
        /// </summary>
        /// <param name="statusType">"live" or "upcoming"</param>
        /// <returns>List of scholarships matching the status</returns>
        Task<IEnumerable<ScholarshipStatusDto>> GetScholarshipsByStatus(string statusType);

        /// <summary>
        /// Get featured scholarships (highest amount, recently added, recently updated)
        /// </summary>
        /// <returns>List of featured scholarships</returns>
        Task<IEnumerable<FeaturedScholarshipDto>> GetFeaturedScholarships();
    }
}
