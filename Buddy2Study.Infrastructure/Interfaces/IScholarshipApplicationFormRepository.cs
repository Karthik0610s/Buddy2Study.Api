using Buddy2Study.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Infrastructure.Interfaces
{
    public interface IScholarshipApplicationFormRepository
    {
        Task<IEnumerable<ScholarshipApplicationForm>> GetScholarshipsApplicationForm(int? id);
        Task<ScholarshipApplicationForm> InsertScholarshipApplicationForm(ScholarshipApplicationForm dto);
        Task UpdateScholarshipApplicationForm(ScholarshipApplicationForm dto);
        Task<bool> DeleteScholarshipApplicationForm(int id);
    }
}
