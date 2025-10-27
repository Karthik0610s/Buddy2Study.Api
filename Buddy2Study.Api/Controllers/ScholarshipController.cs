using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.IO.Compression;

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
                foreach (var Student in result)
                {
                    if (!string.IsNullOrWhiteSpace(Student.FileName))
                    {
                        var filesList = Student.FileName.Split('|').ToList();


                        if (filesList.Count > 0 && string.IsNullOrWhiteSpace(filesList.Last()))
                        {
                            filesList.RemoveAt(filesList.Count - 1);
                        }

                        Student.Files = filesList;
                    }
                }
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetScholarshipById(int id)
        {
            _logger.LogInformation("{MethodName} called with Id {Id}", nameof(GetScholarshipById), id);

            if (id <= 0)
                return BadRequest("Invalid scholarship ID.");

            try
            {
                var scholarship = await _scholarshipService.GetScholarshipById(id);

                if (scholarship == null)
                    return NotFound($"No scholarship found with ID {id}.");

                return Ok(scholarship);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}", nameof(GetScholarshipById));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetScholarshipById));
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
                var updatedScholarship = await _scholarshipService.UpdateScholarship(scholarshipDto);
                return Ok(updatedScholarship); // Return the updated record
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
        /// </summary>
        [HttpGet("downloadFiles/{id}")]
        public async Task<IActionResult> DownloadFiles(int id)
        {
            var scholarship = await _scholarshipService.GetScholarshipById(id);
            //var scholarship = result.FirstOrDefault();

            if (scholarship == null)
                return NotFound("Scholarship not found.");

            if (string.IsNullOrEmpty(scholarship.FilePath) || !Directory.Exists(scholarship.FilePath))
                return NotFound("Files not found.");

            var zipName = $"scholarship_{id}_{DateTime.Now:yyyyMMddHHmmss}.zip";
            using var compressedFileStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in Directory.GetFiles(scholarship.FilePath))
                {
                    var zipEntry = zipArchive.CreateEntry(Path.GetFileName(file));
                    using var fileStream = System.IO.File.OpenRead(file);
                    using var entryStream = zipEntry.Open();
                    await fileStream.CopyToAsync(entryStream);
                }
            }

            return File(compressedFileStream.ToArray(), "application/zip", zipName);
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetByStatus([FromQuery] string statusType)
        {
            _logger.LogInformation("{MethodName} called with StatusType {StatusType}", nameof(GetByStatus), statusType);

            if (string.IsNullOrEmpty(statusType))
                return BadRequest("StatusType parameter is required.");

            try
            {
                var result = await _scholarshipService.GetScholarshipsByStatus(statusType);

                if (result == null || !result.Any())
                    return NotFound($"No scholarships found for StatusType '{statusType}'.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}", nameof(GetByStatus));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetByStatus));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        // GET: api/scholarships/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatured()
        {
            _logger.LogInformation("{MethodName} called", nameof(GetFeatured));

            try
            {
                var result = await _scholarshipService.GetFeaturedScholarships();

                if (result == null || !result.Any())
                    return NotFound("No featured scholarships found.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}", nameof(GetFeatured));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetFeatured));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }
    }
}
