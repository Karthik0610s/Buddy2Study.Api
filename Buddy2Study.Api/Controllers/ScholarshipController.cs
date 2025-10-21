using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Buddy2Study.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScholarshipController : ControllerBase
    {
        private readonly IScholarshipService _scholarshipService;
        private readonly ILogger<ScholarshipController> _logger;

        public ScholarshipController(ILogger<ScholarshipController> logger, IScholarshipService scholarshipService)
        {
            _logger = logger;
            _scholarshipService = scholarshipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetScholarships([FromQuery] int id, [FromQuery] string role)
        {
            _logger.LogInformation("{MethodName} called for role {Role} and id {Id}", nameof(GetScholarships), role, id);

            if (string.IsNullOrEmpty(role))
                return BadRequest("Role parameter is required (student/sponsor).");

            try
            {
                var result = await _scholarshipService.GetScholarshipsDetails(id, role);

                if (result == null || !result.Any())
                    return NotFound("No scholarships found for the given criteria.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}", nameof(GetScholarships));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetScholarships));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertScholarship([FromBody] ScholarshipDto scholarshipDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertScholarship));

            if (scholarshipDto == null)
                return BadRequest("Scholarship data is required.");

            try
            {
                var result = await _scholarshipService.InsertScholarship(scholarshipDto);

                if (result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Insert failed. Stored procedure did not return data.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}: {Message}", nameof(InsertScholarship), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(InsertScholarship));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateScholarship([FromBody] ScholarshipDto scholarshipDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(UpdateScholarship));

            if (scholarshipDto == null || scholarshipDto.Id <= 0)
                return BadRequest("Invalid scholarship data.");

            try
            {
                var existing = await _scholarshipService.GetScholarshipsDetails(scholarshipDto.Id, "sponsor");

                if (!existing.Any())
                    return NotFound($"No scholarship found with ID {scholarshipDto.Id}.");

                await _scholarshipService.UpdateScholarship(scholarshipDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(UpdateScholarship));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "Unexpected error occurred while updating scholarship."
                });
            }
        }
        /// <summary>
        /// Delete a scholarship by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScholarship(int id, [FromQuery] string modifiedBy)
        {
            _logger.LogInformation("{MethodName} called for Id {Id}", nameof(DeleteScholarship), id);

            if (id <= 0)
                return BadRequest("Invalid scholarship ID.");

            if (string.IsNullOrEmpty(modifiedBy))
                return BadRequest("ModifiedBy parameter is required.");

            try
            {
                var result = await _scholarshipService.DeleteScholarship(id, modifiedBy);

                if (!result)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Delete operation failed.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(DeleteScholarship));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "Unexpected error occurred while deleting scholarship."
                });
            }
        }

    }
}
