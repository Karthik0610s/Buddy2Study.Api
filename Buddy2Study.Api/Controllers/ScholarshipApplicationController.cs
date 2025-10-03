using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Net;

namespace Buddy2Study.Api.Controllers
{/// <summary>
 /// Controller for handling CRUD operations on Scholarship Application Form.
 /// </summary>
    [Route("api/scholarshipApplicationForm")]
    [ApiController]
   // [Authorize]
    public class ScholarshipApplicationController : BaseController
    {
        private readonly IScholarshipApplicationFormService _scholarshipService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScholarshipApplicationController"/> class.
        /// </summary>
        public ScholarshipApplicationController(ILogger<ScholarshipApplicationController> logger, IScholarshipApplicationFormService scholarshipService)
            : base(logger)
        {
            _scholarshipService = scholarshipService;
        }

        /// <summary>
        /// Retrieves all scholarships.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ScholarshipApplicationFormDto>))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllScholarshipsApplicationForm()
        {
            _logger.LogInformation("{MethodName} called", nameof(GetAllScholarshipsApplicationForm));
            try
            {
                var result = await _scholarshipService.GetScholarshipsApplicationForm(null);
                if (result == null || !result.Any())
                    return NoContent();
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
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Retrieves scholarship by Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ScholarshipApplicationFormDto))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetScholarshipApplicationFormById(int id)
        {
            _logger.LogInformation("{MethodName} called for Id: {id}", nameof(GetScholarshipApplicationFormById), id);

            if (id < 1)
                return BadRequest("Invalid Scholarship Id");

            try
            {
                var result = await _scholarshipService.GetScholarshipsApplicationForm(id);
                return result.Any() ? Ok(result.First()) : NotFound("Scholarship not found.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Inserts a new scholarship.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> InsertScholarshipDApplicationForm([FromBody] ScholarshipApplicationFormDto ScholarshipApplicationFormDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertScholarshipDApplicationForm));
            try
            {
                var scholarship = await _scholarshipService.InsertScholarshipApplicationForm(ScholarshipApplicationFormDto);
                return CreatedAtAction(nameof(GetScholarshipApplicationFormById), new { id = scholarship.Id }, scholarship);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error");
                return DatabaseError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Updates an existing scholarship.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateScholarshipApplicationForm([FromBody] ScholarshipApplicationFormDto ScholarshipApplicationFormDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(UpdateScholarshipApplicationForm));
            try
            {
                var existing = await _scholarshipService.GetScholarshipsApplicationForm(ScholarshipApplicationFormDto.Id);
                if (existing == null || !existing.Any())
                    return NotFound();

                await _scholarshipService.UpdateScholarshipApplicationForm(ScholarshipApplicationFormDto);
                return NoContent();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error");
                return DatabaseError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Deletes a scholarship by Id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteScholarshipApplicationForm(int id)
        {
            _logger.LogInformation("{MethodName} called for Id: {id}", nameof(DeleteScholarshipApplicationForm), id);
            try
            {
                var existing = await _scholarshipService.GetScholarshipsApplicationForm(id);
                if (existing == null || !existing.Any())
                    return NotFound();

                await _scholarshipService.DeleteScholarshipApplicationForm(id);
                return NoContent();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error");
                return DatabaseError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Downloads scholarship-related files as a zip.
        /// </summary>
        [HttpGet("downloadFiles/{id}")]
        public async Task<IActionResult> DownloadFiles(int id)
        {
            var result = await _scholarshipService.GetScholarshipsApplicationForm(id);
            var scholarship = result.FirstOrDefault();

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

        
        private ObjectResult DatabaseError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ProblemDetails
            {
                Title = "Database Error",
                Detail = "An error occurred while processing your request. Please try again later.",
                Status = (int)HttpStatusCode.InternalServerError
            });
        }

        [HttpPost("UploadFiles")]
        [ProducesResponseType(200, Type = typeof(FileUploadDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UploadfacultystudentFile([FromForm] FileUploadDto fileUploadModel)
        {
            if (fileUploadModel.FormFiles != null)
            {
                var target = Path.Combine(Directory.GetCurrentDirectory(), fileUploadModel.TypeofUser, fileUploadModel.TypeofUser + "-" + fileUploadModel.Id);

                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                // Get existing files in the directory
                List<string> existingFiles = Directory.GetFiles(target).Select(Path.GetFileName).ToList();

                // Upload new files
                for (int i = 0; i < fileUploadModel.FormFiles.Count; i++)
                {
                    string path = Path.Combine(target, fileUploadModel.FormFiles[i].FileName);
                    // Check if file already exists to prevent duplication
                    if (!System.IO.File.Exists(path))
                    {
                        using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            await fileUploadModel.FormFiles[i].CopyToAsync(stream);
                        }

                        // Add new file name to the list
                        existingFiles.Add(fileUploadModel.FormFiles[i].FileName);
                    }
                }

                // Join filenames with '|'
                string filenames = string.Join("|", existingFiles) + "|";

                // Update file path data in the database
                var result = await _scholarshipService.UpdateFilepathdata(target, fileUploadModel.Id, filenames, fileUploadModel.TypeofUser);
            }

            return Ok();
        }
    }
}

