using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Buddy2Study.Api.Controllers
{
    [ApiController]
    [Route("api/Insutition")]
    public class InstitutionController : Controller
    {
      
    
       
       
            private readonly IInstitutionService _InstitutionService;
            private readonly ILogger<InstitutionController> _logger;


            public InstitutionController(ILogger<InstitutionController> logger, IInstitutionService InstitutionService)
            {
                _logger = logger;
                _InstitutionService = InstitutionService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllInstitutions()
            {
                _logger.LogInformation("{MethodName} called", nameof(GetAllInstitutions));
                try
                {
                    var result = await _InstitutionService.GetInstitutionsDetails(null);
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
            public async Task<IActionResult> GetInstitutionById(int id)
            {
                _logger.LogInformation("{MethodName} called for id {id}", nameof(GetInstitutionById), id);

                if (id < 1)
                    return BadRequest();

                try
                {
                    var InstitutionDtos = await _InstitutionService.GetInstitutionsDetails(id);
                    var Institution = InstitutionDtos.FirstOrDefault();

                    if (Institution == null)
                        return NotFound();

                    return Ok(Institution);
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
        public async Task<IActionResult> InsertInstitution([FromBody] InstitutionDto InstitutionDto)
        {
            _logger.LogInformation("{MethodName} called", nameof(InsertInstitution));

            if (InstitutionDto == null)
                return BadRequest(new { error = true, message = "Institution data is required." });

            try
            {
                var InstitutionDetail = await _InstitutionService.InsertInstitutionDetails(InstitutionDto);

                // Check if SP returned an error message (ErrorMessage property in model)
                if (InstitutionDetail == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new { error = true, message = "Insert failed. Stored procedure did not return data." });

                if (!string.IsNullOrEmpty(InstitutionDetail.ErrorMessage))
                {
                    // Return the SP error to frontend
                    return BadRequest(new
                    {
                        error = true,
                        message = InstitutionDetail.ErrorMessage
                    });
                }

                // Success response
                return Ok(new
                {
                    error = false,
                    message = "Institution inserted successfully",
                    data = InstitutionDetail
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);

                // Any unexpected exception
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    error = true,
                    message = ex.Message
                });
            }
        }


        [HttpPut]
            public async Task<IActionResult> UpdateInstitution([FromBody] InstitutionDto InstitutionDto)
            {
                _logger.LogInformation("{MethodName} called", nameof(UpdateInstitution));

                var InstitutionDtos = await _InstitutionService.GetInstitutionsDetails(InstitutionDto.InstitutionID);
                if (!InstitutionDtos.Any())
                    return NotFound();

                try
                {
                    await _InstitutionService.UpdateInstitutionDetails(InstitutionDto);
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
