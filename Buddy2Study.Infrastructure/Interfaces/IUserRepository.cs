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
        Task<Users> InsertUserDetails(Users users);
        /// <summary>
        /// Updates an existing Users.
        /// </summary>
        /// <param name="users">The Users to update.</param>
        /// <returns>
        /// Not returns anything.
        /// </returns>
        Task UpdateUserDetails(Users users);
        /// <summary>
        /// Deletes a Users by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Users to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteUserDetails(int id);
        //Task LoginAsync(string username, string password);

        /// <summary>
        /// reset a labour password.
        /// </summary>
        /// <param name="username">The username of the labour to update.</param>
        /// <param name="password">The password of the labour to update.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        //   Task ResetPasswordAsync(int id, string username, string newPassword, string modifiedBy);
    }
}
