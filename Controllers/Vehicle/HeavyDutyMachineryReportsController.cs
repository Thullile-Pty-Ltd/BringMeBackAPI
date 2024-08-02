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
    public class HeavyDutyMachineryReportsController : ControllerBase
    {
        private readonly IHeavyDutyMachineryReportService _heavyDutyMachineryReportService;
        private readonly IConfiguration _configuration;

        public HeavyDutyMachineryReportsController(IHeavyDutyMachineryReportService heavyDutyMachineryReportService, IConfiguration configuration)
        {
            _heavyDutyMachineryReportService = heavyDutyMachineryReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeavyDutyMachineryReport(int id)
        {
            var report = await _heavyDutyMachineryReportService.GetHeavyDutyMachineryReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHeavyDutyMachineryReport([FromBody] HeavyDutyMachinery report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _heavyDutyMachineryReportService.CreateHeavyDutyMachineryReport(userId, report);
            return CreatedAtAction(nameof(GetHeavyDutyMachineryReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateHeavyDutyMachineryReport(int id, [FromBody] HeavyDutyMachinery report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _heavyDutyMachineryReportService.UpdateHeavyDutyMachineryReport(userId, id, report);
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
