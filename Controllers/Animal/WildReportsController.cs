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
    public class WildReportsController : ControllerBase
    {
        private readonly IWildReportService _wildReportService;
        private readonly IConfiguration _configuration;

        public WildReportsController(IWildReportService wildReportService, IConfiguration configuration)
        {
            _wildReportService = wildReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWildReport(int id)
        {
            var report = await _wildReportService.GetWildReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWildReport([FromBody] Wild report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _wildReportService.CreateWildReport(userId, report);
            return CreatedAtAction(nameof(GetWildReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateWildReport(int id, [FromBody] Wild report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _wildReportService.UpdateWildReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

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
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        private string GenerateJWTToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
        };

            var secretKey = _configuration["Jwt:Key"];
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
