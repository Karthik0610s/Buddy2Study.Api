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

    }
}
