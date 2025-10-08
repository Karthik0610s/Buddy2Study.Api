using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

namespace Buddy2Study.Infrastructure.Interfaces
{

    public interface IStudentRepository
    {
        /// <summary>
        /// Get all students or a specific student by ID.
        /// </summary>
        Task<IEnumerable<Students>> GetStudentsDetails(int? id);

        /// <summary>
        /// Insert a new student record.
        /// </summary>
        Task<Students> InsertStudentDetails(Students student);

        /// <summary>
        /// Update an existing student record.
        /// </summary>
        Task UpdateStudentDetails(Students student);
    }
}
