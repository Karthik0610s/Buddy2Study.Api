using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Buddy2Study.Api.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            _logger.LogInformation("{MethodName} called", nameof(GetAllStudents));
            try
            {
                var result = await _studentService.GetStudentsDetails(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetAllStudents));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

        /// <summary>
        /// Get a student by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            _logger.LogInformation("{MethodName} called for id {id}", nameof(GetStudentById), id);

            if (id < 1)
                return BadRequest("Invalid student ID.");

            try
            {
                var studentDtos = await _studentService.GetStudentsDetails(id);
                var student = studentDtos.FirstOrDefault();

                if (student == null)
                    return NotFound($"No student found with ID {id}.");

                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(GetStudentById));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Add a new student.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertStudent([FromBody] StudentDto studentDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertStudent));

            if (studentDto == null)
                return BadRequest("Student data is required.");

            try
            {
                var result = await _studentService.InsertStudentDetails(studentDto);

                if (result == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Insert failed. Stored procedure did not return data.");

                return Ok(result);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL Error in {MethodName}: {Message}", nameof(InsertStudent), ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Database Error",
                    Detail = ex.ToString(),
                    Status = StatusCodes.Status500InternalServerError
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(InsertStudent));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Update an existing student record.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto studentDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(UpdateStudent));

            if (studentDto == null || studentDto.Id <= 0)
                return BadRequest("Invalid student data.");

            try
            {
                var existing = await _studentService.GetStudentsDetails(studentDto.Id);
                if (!existing.Any())
                    return NotFound($"No student found with ID {studentDto.Id}.");

                await _studentService.UpdateStudentDetails(studentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {MethodName}", nameof(UpdateStudent));
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "Unexpected error occurred while updating student."
                });
            }
        }
    }
}
