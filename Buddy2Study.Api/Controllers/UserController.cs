using System.Data.SqlClient;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlException = Microsoft.Data.SqlClient.SqlException;


namespace Buddy2Study.Api.Controllers
{
    [ApiController]
    [Route("api/userDto")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;


        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("{MethodName} called", nameof(GetAllUsers));
            try
            {
                var result = await _userService.GetUsersDetails(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation("{MethodName} called for id {id}", nameof(GetUserById), id);

            if (id < 1)
                return BadRequest();

            try
            {
                var userDtos = await _userService.GetUsersDetails(id);
                var user = userDtos.FirstOrDefault();

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserDto userDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertUser));

            if (userDto == null)
                return BadRequest("User data is required.");

            try
            {
                var userDetail = await _userService.InsertUserDetails(userDto);

                if (userDetail == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Insert failed. Stored procedure did not return data.");

                return Ok(userDetail); // 👈 instead of CreatedAtAction
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.ToString(),  // 👈 this will show full SQL exception details
                    Status = StatusCodes.Status500InternalServerError
                });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(UpdateUser));

            var userDtos = await _userService.GetUsersDetails(userDto.Id);
            if (!userDtos.Any())
                return NotFound();

            try
            {
                await _userService.UpdateUserDetails(userDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "Unexpected error."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("{MethodName} called for id {id}", nameof(DeleteUser), id);

            var userDtos = await _userService.GetUsersDetails(id);
            if (!userDtos.Any())
                return NotFound();

            try
            {
                await _userService.DeleteUserDetails(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "Unexpected error."
                });
            }
        }
    }
}
