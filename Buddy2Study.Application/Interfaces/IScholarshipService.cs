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
    }
}
