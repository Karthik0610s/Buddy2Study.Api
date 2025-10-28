using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy2Study.Domain.Entities;

namespace Buddy2Study.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a list of user details. 
        /// If 'id' is provided, returns a specific user; otherwise returns all users.
        /// </summary>
        /// <param name="id">Optional user ID.</param>
        /// <returns>List of users.</returns>
        Task<IEnumerable<Users>> GetUsersDetails(int? id);

        /// <summary>
        /// Retrieves a single user record by email.
        /// </summary>
        /// <param name="email">Email address of the user.</param>
        /// <returns>User entity if found, otherwise null.</returns>
        Task<Users?> GetByEmailAsync(string email);
    }
}
