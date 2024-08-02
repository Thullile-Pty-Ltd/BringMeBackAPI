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
    public class PetReportsController : ControllerBase
    {
        private readonly IPetReportService _petReportService;
        private readonly IConfiguration _configuration;

        public PetReportsController(IPetReportService petReportService, IConfiguration configuration)
        {
            _petReportService = petReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetReport(int id)
        {
            var report = await _petReportService.GetPetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePetReport([FromBody] Pet report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _petReportService.CreatePetReport(userId, report);
            return CreatedAtAction(nameof(GetPetReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePetReport(int id, [FromBody] Pet report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _petReportService.UpdatePetReport(userId, id, report);
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
