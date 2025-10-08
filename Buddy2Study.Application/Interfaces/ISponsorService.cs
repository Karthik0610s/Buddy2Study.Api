using Buddy2Study.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Interfaces
{
    /// <summary>
    /// Service interface for Sponsor operations.
    /// </summary>
    public interface ISponsorService
    {
        /// <summary>
        /// Retrieves Sponsors optionally filtered by their unique identifier.
        /// </summary>
        /// <param name="id">Optional. The unique identifier of the Sponsor to retrieve. If not provided, retrieves all Sponsors.</param>
        /// <returns>A collection of SponsorDto objects if successful, or null if none found.</returns>
        Task<IEnumerable<SponsorDto>> GetSponsorsDetails(int? id);

        /// <summary>
        /// Inserts a new Sponsor.
        /// </summary>
        /// <param name="SponsorDto">The DTO representing the Sponsor to insert.</param>
        /// <returns>The inserted SponsorDto if successful.</returns>
        Task<SponsorDto> InsertSponsorDetails(SponsorDto SponsorDto);

        /// <summary>
        /// Updates an existing Sponsor.
        /// </summary>
        /// <param name="SponsorDto">The DTO representing the updated Sponsor data.</param>
        /// <returns>The updated SponsorDto if successful.</returns>
        Task<SponsorDto> UpdateSponsorDetails(SponsorDto SponsorDto);
    }
}
