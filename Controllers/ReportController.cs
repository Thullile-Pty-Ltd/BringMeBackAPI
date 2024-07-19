using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using BringMeBackAPI.Models.Users;

namespace BringMeBackAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IConfiguration _configuration;

        public ReportsController(IReportService reportService, IConfiguration configuration)
        {
            _reportService = reportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var report = await _reportService.GetReportById(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _reportService.CreateReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] Report report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _reportService.UpdateReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> ArchiveReport(int id)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var archived = await _reportService.ArchiveReport(userId, id);
                if (archived)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // New endpoint to generate JWT token
        [AllowAnonymous]
        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user");
            }

            var token = GenerateJWTToken(user);
            return Ok(new { Token = token });
        }

        private int GetUserIdFromClaims()
        {
            // Extract userId from claims, assuming userId is part of the claims
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        public string GenerateJWTToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
    };

            var secretKey = _configuration["Jwt:Key"]; // Update to match your configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
