using Buddy2Study.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Interfaces
{
    public interface IScholarshipApplicationFormService
    {
        Task<IEnumerable<ScholarshipApplicationFormDto>> GetScholarshipsApplicationForm(int? id);
        Task<ScholarshipApplicationFormDto> InsertScholarshipApplicationForm(ScholarshipApplicationFormDto dto);
        Task UpdateScholarshipApplicationForm(ScholarshipApplicationFormDto dto);
        Task<bool> DeleteScholarshipApplicationForm(int id);
        Task<String> UpdateFilepathdata(string target, int id, string files, string TypeofUser);

    }

}
