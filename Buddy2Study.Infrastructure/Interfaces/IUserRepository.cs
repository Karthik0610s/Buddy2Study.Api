using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

namespace Buddy2Study.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersDetails(int? id);
        /// <summary>
        /// Inserts a new Users.
        /// </summary>
        /// <param name="users">The Users to insert.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>

    }
}
