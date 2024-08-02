using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Reports.Animal;
using BringMeBackAPI.Services.Reports.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BringMeBackAPI.Controllers.Other
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DangerZoneReportsController : ControllerBase
    {
        private readonly IDangerZoneReportService _DangerZoneReportService;
        private readonly IConfiguration _configuration;

        public DangerZoneReportsController(IDangerZoneReportService DangerZoneReportService, IConfiguration configuration)
        {
            _DangerZoneReportService = DangerZoneReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDangerZoneReport(int id)
        {
            var report = await _DangerZoneReportService.GetDangerZoneReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWildReport([FromBody] DangerZone report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _DangerZoneReportService.CreateDangerZoneReport(userId, report);
            return CreatedAtAction(nameof(GetDangerZoneReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDangerZoneReport(int id, [FromBody] DangerZone report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _DangerZoneReportService.UpdateDangerZoneReport(userId, id, report);
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
