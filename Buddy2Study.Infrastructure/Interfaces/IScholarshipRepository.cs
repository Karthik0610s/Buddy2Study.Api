using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

public interface IScholarshipRepository
{
    Task<IEnumerable<Scholarships>> GetScholarshipsDetails(int id, string role);
    Task<Scholarships> GetScholarshipById(int id); 

    Task<Scholarships> InsertScholarship(Scholarships scholarship);
    Task<Scholarships> UpdateScholarship(Scholarships scholarship);
    Task DeleteScholarship(int id, string modifiedBy);
    /// <summary>
    /// Get scholarships by status (live or upcoming)
    /// </summary>
    /// <param name="statusType">"live" or "upcoming"</param>
    /// <returns>List of scholarships matching the status</returns>
    Task<IEnumerable<ScholarshipStatus>> GetScholarshipsByStatus(string statusType);

    /// <summary>
    /// Get featured scholarships (highest amount, recently added, recently updated)
    /// </summary>
    /// <returns>List of featured scholarships</returns>
    Task<IEnumerable<FeaturedScholarship>> GetFeaturedScholarships();
}
