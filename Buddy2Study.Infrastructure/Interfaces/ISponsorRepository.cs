using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

namespace Buddy2Study.Infrastructure.Interfaces
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<Sponsors>> GetSponsorsDetails(int? id);
        /// <summary>
        /// Inserts a new Sponsors.
        /// </summary>
        /// <param name="Sponsors">The Sponsors to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Sponsors> InsertSponsorDetails(Sponsors Sponsors);
        /// <summary>
        /// Updates an existing Sponsors.
        /// </summary>
        /// <param name="Sponsors">The Sponsors to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
Task<Sponsors> UpdateSponsorDetails(Sponsors sponsor);
     

        /// <summary>
        /// Deletes a Sponsors by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Sponsors to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>

    }
}
