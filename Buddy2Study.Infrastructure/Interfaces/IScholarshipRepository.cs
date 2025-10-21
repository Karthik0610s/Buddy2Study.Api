using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

public interface IScholarshipRepository
{
    Task<IEnumerable<Scholarships>> GetScholarshipsDetails(int id, string role);
    Task<Scholarships> InsertScholarship(Scholarships scholarship);
    Task<Scholarships> UpdateScholarship(Scholarships scholarship);
    Task DeleteScholarship(int id, string modifiedBy);
}
