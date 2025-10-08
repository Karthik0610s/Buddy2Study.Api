using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{
    public interface IStudentService
    {
         
        Task<IEnumerable<StudentDto>> GetStudentsDetails(int? id);
        Task<StudentDto> InsertStudentDetails(StudentDto student);
        Task UpdateStudentDetails(StudentDto student);
    }
}

