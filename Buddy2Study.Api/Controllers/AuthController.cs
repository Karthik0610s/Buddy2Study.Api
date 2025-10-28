using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Buddy2Study.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Normal user login endpoint
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
        [AllowAnonymous]
        [HttpGet("{provider}/login")]
        public IActionResult ExternalLogin([FromRoute] string provider, string? returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { provider, returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        [HttpGet("{provider}/callback")]
        public async Task<IActionResult> ExternalLoginCallback(string provider, string? returnUrl = "/")
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest("External authentication failed.");

            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
            var name = claims?.FirstOrDefault(c => c.Type.Contains("name"))?.Value;

            // If user doesn't exist, register or create them
            var token = await _authService.LoginOrRegisterExternalUserAsync(email, name, provider);

            // Redirect back to React frontend with token in query
            var clientUrl = _configuration["Frontend:BaseUrl"];
            return Redirect($"{clientUrl}/login-success?token={token.Token}&name={Uri.EscapeDataString(name ?? "")}");
        }


    }
}
