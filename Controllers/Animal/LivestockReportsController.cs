using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Reports.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BringMeBackAPI.Controllers.Animal
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LivestockReportsController : ControllerBase
    {
        private readonly ILivestockReportService _livestockReportService;
        private readonly IConfiguration _configuration;

        public LivestockReportsController(ILivestockReportService livestockReportService, IConfiguration configuration)
        {
            _livestockReportService = livestockReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLivestockReport(int id)
        {
            var report = await _livestockReportService.GetLivestockReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLivestockReport([FromBody] Livestock report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _livestockReportService.CreateLivestockReport(userId, report);
            return CreatedAtAction(nameof(GetLivestockReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateLivestockReport(int id, [FromBody] Livestock report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _livestockReportService.UpdateLivestockReport(userId, id, report);
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
