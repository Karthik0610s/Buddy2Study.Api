using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Buddy2Study.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        /// <summary>
        /// Login endpoint.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(TokenDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody][Required] LoginUserDto user)
        {
            if (string.IsNullOrEmpty(user.Username))
                return BadRequest(new ProblemDetails { Title = "Bad Request", Detail = "Username cannot be empty." });

            if (string.IsNullOrEmpty(user.Password))
                return BadRequest(new ProblemDetails { Title = "Bad Request", Detail = "Password cannot be empty." });

            try
            {
                var tokenDto = await _authService.LoginAsync(user.Username, user.Password);
                return Ok(tokenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }
    }
}
