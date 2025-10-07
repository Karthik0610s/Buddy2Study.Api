using Buddy2Study.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Interfaces
{
    public interface IInstitutionService
    {
        /// <summary>
        /// Retrieves Institutions optionally filtered by their unique identifier.
        /// </summary>
        /// <param// name="id">Optional. The unique identifier of the InstitutionDto to retrieve. If not provided, retrieves all Institutions.</param>
        /// <returns>
        /// The task result contains a collection of InstitutionDto DTOs. if successful, or null if no Institutions match the provided identifier.
        /// </returns>
        Task<IEnumerable<InstitutionDto>> GetInstitutionsDetails(int? id);
        /// <summary>
        /// Inserts a new InstitutionDto.
        /// </summary>
        /// <param// name="InstitutionDto">The DTO representing the InstitutionDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<InstitutionDto> InsertInstitutionDetails(InstitutionDto InstitutionDto);

        /// <summary>
        /// Updates an existing InstitutionDto.
        /// </summary>
        /// <param //name="InstitutionDto">The DTO representing the updated InstitutionDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateInstitutionDetails(InstitutionDto InstitutionDto);
        /// <summary>
        /// Deletes a InstitutionDto by its unique identifier.
        /// </summary>
        /// <param //name="id">The unique identifier of the InstitutionDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>

    }
}
