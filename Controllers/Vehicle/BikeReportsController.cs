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
    public class BikeReportsController : ControllerBase
    {
        private readonly IBikeReportService _bikeReportService;
        private readonly IConfiguration _configuration;

        public BikeReportsController(IBikeReportService bikeReportService, IConfiguration configuration)
        {
            _bikeReportService = bikeReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBikeReport(int id)
        {
            var report = await _bikeReportService.GetBikeReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBikeReport([FromBody] Bike report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _bikeReportService.CreateBikeReport(userId, report);
            return CreatedAtAction(nameof(GetBikeReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBikeReport(int id, [FromBody] Bike report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _bikeReportService.UpdateBikeReport(userId, id, report);
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
