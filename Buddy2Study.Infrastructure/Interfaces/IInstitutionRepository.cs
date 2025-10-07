using Buddy2Study.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Interfaces
{
    public  interface IInstitutionRepository
    {

        Task<IEnumerable<Institution>> GetInstitutionsDetails(int? id);
        /// <summary>
        /// Inserts a new Institutions.
        /// </summary>
        /// <param name="Institutions">The Institutions to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task<Institution> InsertInstitutionDetails(Institution Institutions);
        /// <summary>
        /// Updates an existing Institutions.
        /// </summary>
        /// <param name="Institutions">The Institutions to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateInstitutionDetails(Institution Institutions);
        /// <summary>
        /// Deletes a Institutions by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Institutions to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
    }
}
