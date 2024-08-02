using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Services.Reports.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BringMeBackAPI.Controllers.Vehicle
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarReportsController : ControllerBase
    {
        private readonly ICarReportService _carReportService;
        private readonly IConfiguration _configuration;

        public CarReportsController(ICarReportService carReportService, IConfiguration configuration)
        {
            _carReportService = carReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarReport(int id)
        {
            var report = await _carReportService.GetCarReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCarReport([FromBody] Car report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _carReportService.CreateCarReport(userId, report);
            return CreatedAtAction(nameof(GetCarReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCarReport(int id, [FromBody] Car report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _carReportService.UpdateCarReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        private int GetUserIdFromClaims()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
    }

}
