using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{
    public interface IScholarshipService
    {
        Task<IEnumerable<ScholarshipDto>> GetScholarshipsDetails(int id, string role);
        Task<ScholarshipDto> InsertScholarship(ScholarshipDto scholarship);
        Task UpdateScholarship(ScholarshipDto scholarship);

        /// <summary>
        /// Delete a scholarship by Id.
        /// </summary>
        Task DeleteScholarship(int id, string modifiedBy);
    }
}
