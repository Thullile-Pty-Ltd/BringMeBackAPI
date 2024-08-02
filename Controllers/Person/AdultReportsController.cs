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
    public class AdultReportsController : ControllerBase
    {
        private readonly IAdultReportService _adultReportService;
        private readonly IConfiguration _configuration;

        public AdultReportsController(IAdultReportService adultReportService, IConfiguration configuration)
        {
            _adultReportService = adultReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdultReport(int id)
        {
            var report = await _adultReportService.GetAdultReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAdultReport([FromBody] Adult report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _adultReportService.CreateAdultReport(userId, report);
            return CreatedAtAction(nameof(GetAdultReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAdultReport(int id, [FromBody] Adult report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _adultReportService.UpdateAdultReport(userId, id, report);
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
