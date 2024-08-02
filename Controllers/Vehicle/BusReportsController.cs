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
    public class BusReportsController : ControllerBase
    {
        private readonly IBusReportService _busReportService;
        private readonly IConfiguration _configuration;

        public BusReportsController(IBusReportService busReportService, IConfiguration configuration)
        {
            _busReportService = busReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusReport(int id)
        {
            var report = await _busReportService.GetBusReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBusReport([FromBody] Bus report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _busReportService.CreateBusReport(userId, report);
            return CreatedAtAction(nameof(GetBusReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBusReport(int id, [FromBody] Bus report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _busReportService.UpdateBusReport(userId, id, report);
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
