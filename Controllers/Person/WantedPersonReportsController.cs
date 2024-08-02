using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Reports.Person;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BringMeBackAPI.Controllers.Person
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WantedPersonReportsController : ControllerBase
    {
        private readonly IWantedPersonReportService _wantedPersonReportService;
        private readonly IConfiguration _configuration;

        public WantedPersonReportsController(IWantedPersonReportService wantedPersonReportService, IConfiguration configuration)
        {
            _wantedPersonReportService = wantedPersonReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWantedPersonReport(int id)
        {
            var report = await _wantedPersonReportService.GetWantedPersonReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWantedPersonReport([FromBody] WantedPerson report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _wantedPersonReportService.CreateWantedPersonReport(userId, report);
            return CreatedAtAction(nameof(GetWantedPersonReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWantedPersonReport(int id, [FromBody] WantedPerson report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _wantedPersonReportService.UpdateWantedPersonReport(userId, id, report);
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
