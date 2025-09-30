using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buddy2Study.Application.Dtos;

namespace Buddy2Study.Application.Interfaces
{

    //  Task GetUsersDetails(object value);

    // Task InsertUserDetails(UserDto userDto);

    public interface IUserService
    {/// <summary>
     /// Retrieves Users optionally filtered by their unique identifier.
     /// </summary>
     /// <param// name="id">Optional. The unique identifier of the UserDto to retrieve. If not provided, retrieves all Users.</param>
     /// <returns>
     /// The task result contains a collection of UserDto DTOs. if successful, or null if no Users match the provided identifier.
     /// </returns>
        Task<IEnumerable<UserDto>> GetUsersDetails(int? id);
        /// <summary>
        /// Inserts a new UserDto.
        /// </summary>
        /// <param// name="userDto">The DTO representing the UserDto to insert.</param>
        /// <returns>
        /// The task result indicates whether the insertion was successful.
        /// </returns>
        Task<UserDto> InsertUserDetails(UserDto userDto);

        /// <summary>
        /// Updates an existing UserDto.
        /// </summary>
        /// <param //name="userDto">The DTO representing the updated UserDto.</param>
        /// <returns>
        ///The task result indicates whether the update was successful.
        /// </returns>
        Task UpdateUserDetails(UserDto userDto);
        /// <summary>
        /// Deletes a UserDto by its unique identifier.
        /// </summary>
        /// <param //name="id">The unique identifier of the UserDto to delete.</param>
        /// <returns>
        /// The task result indicates whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteUserDetails(int id);
    }
}

