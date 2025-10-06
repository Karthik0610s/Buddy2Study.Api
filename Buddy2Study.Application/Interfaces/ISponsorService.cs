using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{

    //  Task GetSponsorsDetails(object value);

    // Task InsertSponsorDetails(SponsorDto SponsorDto);

    public interface ISponsorService
    {/// <summary>
     /// Retrieves Sponsors optionally filtered by their unique identifier.
     /// </summary>
     /// <param// name="id">Optional. The unique identifier of the SponsorDto to retrieve. If not provided, retrieves all Sponsors.</param>
     /// <returns>
     /// The task result contains a collection of SponsorDto DTOs. if successful, or null if no Sponsors match the provided identifier.
     /// </returns>
        Task<IEnumerable<SponsorDto>> GetSponsorsDetails(int? id);
        /// <summary>
        /// Inserts a new SponsorDto.
        /// </summary>
        /// <param// name="SponsorDto">The DTO representing the SponsorDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<SponsorDto> InsertSponsorDetails(SponsorDto SponsorDto);

        /// <summary>
        /// Updates an existing SponsorDto.
        /// </summary>
        /// <param //name="SponsorDto">The DTO representing the updated SponsorDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateSponsorDetails(SponsorDto SponsorDto);
        /// <summary>
        /// Deletes a SponsorDto by its unique identifier.
        /// </summary>
        /// <param //name="id">The unique identifier of the SponsorDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
       
    }
}

