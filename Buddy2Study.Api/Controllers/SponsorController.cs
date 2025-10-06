using System.Data.SqlClient;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlException = Microsoft.Data.SqlClient.SqlException;


namespace Buddy2Study.Api.Controllers
{
    [ApiController]
    [Route("api/SponsorDto")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _SponsorService;
        private readonly ILogger<SponsorController> _logger;


        public SponsorController(ILogger<SponsorController> logger, ISponsorService SponsorService)
        {
            _logger = logger;
            _SponsorService = SponsorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSponsors()
        {
            _logger.LogInformation("{MethodName} called", nameof(GetAllSponsors));
            try
            {
                var result = await _SponsorService.GetSponsorsDetails(null);
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
        public async Task<IActionResult> GetSponsorById(int id)
        {
            _logger.LogInformation("{MethodName} called for id {id}", nameof(GetSponsorById), id);

            if (id < 1)
                return BadRequest();

            try
            {
                var SponsorDtos = await _SponsorService.GetSponsorsDetails(id);
                var Sponsor = SponsorDtos.FirstOrDefault();

                if (Sponsor == null)
                    return NotFound();

                return Ok(Sponsor);
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
        public async Task<IActionResult> InsertSponsor([FromBody] SponsorDto SponsorDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertSponsor));

            if (SponsorDto == null)
                return BadRequest("Sponsor data is required.");

            try
            {
                var SponsorDetail = await _SponsorService.InsertSponsorDetails(SponsorDto);

                if (SponsorDetail == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Insert failed. Stored procedure did not return data.");

                return Ok(SponsorDetail); // 👈 instead of CreatedAtAction
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
        public async Task<IActionResult> UpdateSponsor([FromBody] SponsorDto SponsorDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(UpdateSponsor));

            var SponsorDtos = await _SponsorService.GetSponsorsDetails(SponsorDto.Id);
            if (!SponsorDtos.Any())
                return NotFound();

            try
            {
                await _SponsorService.UpdateSponsorDetails(SponsorDto);
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
