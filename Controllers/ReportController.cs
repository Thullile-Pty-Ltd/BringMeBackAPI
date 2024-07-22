using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;

namespace BringMeBackAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMissingPersonReportService _missingPersonReportService;
        private readonly IMissingItemReportService _missingItemReportService;
        private readonly IConfiguration _configuration;
        private readonly IFoundPersonReportService _foundPersonReportService;
        private readonly IFoundItemReportService _foundItemReportService;

        public ReportsController(IReportService reportService, IConfiguration configuration, 
            IMissingPersonReportService missingPersonReportService, 
            IMissingItemReportService missingItemReportService, 
            IFoundPersonReportService foundPersonReportService,
            IFoundItemReportService foundItemReportService)
        {
            _reportService = reportService;
            _configuration = configuration;
            _missingPersonReportService = missingPersonReportService;
            _missingItemReportService = missingItemReportService;
            _foundPersonReportService = foundPersonReportService;
            _foundItemReportService = foundItemReportService;
        }

        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///             CREATE ALL
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("shortreport")]        
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _reportService.CreateReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        ///             CREAT MISSING PERSON
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("CreateMissingPersonReport")]        
        public async Task<IActionResult> CreateMissingPersonReport([FromBody] MissingPersonReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _missingPersonReportService.CreateMissingPersonReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        ///             CREATE MISSING ITEM
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("CreateMissingItemReport")]
        public async Task<IActionResult> CreateMissingItemReport([FromBody] MissingItemReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _missingItemReportService.CreateMissingItemReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        ///             CREATE FOUND PERSON
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("CreateFoundPersonReport")]
        public async Task<IActionResult> CreateFoundPersonReport([FromBody] FoundPersonReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _foundPersonReportService.CreateFoundPersonReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        ///             CREATE FOUND ITEM
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("CreateFoundItemReport")]
        public async Task<IActionResult> CreateFoundItemReport([FromBody] FoundItemReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _foundItemReportService.CreateFoundItemReport(userId, report);
            return CreatedAtAction(nameof(GetReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        ///             EDIT / UPDATE SHORT REPORT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("updatershortreport/{id}")]
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

        /// <summary>
        ///             EDIT / UPDATE MISSING PERSON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("updatemissingperson/{id}")]
        public async Task<IActionResult> UpdateMissingPersonReport(int id, [FromBody] MissingPersonReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _missingPersonReportService.UpdateMissingPersonReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        ///             EDIT / UPDATE MISSING ITEM
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("updatemissingitem/{id}")]
        public async Task<IActionResult> UpdateMissingItemReport(int id, [FromBody] MissingItemReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _missingItemReportService.UpdateMissingItemReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        ///             EDIT / UPDATE FOUND PERSON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("updatefoundperson/{id}")]
        public async Task<IActionResult> UpdateFoundPersonReport(int id, [FromBody] FoundPersonReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _foundPersonReportService.UpdateFoundPersonReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        ///             EDIT / UPDATE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("updatefounditem/{id}")]
        public async Task<IActionResult> UpdateFoundItemReport(int id, [FromBody] FoundItemReport report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _foundItemReportService.UpdateFoundItemReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        ///             DELETE / ARCHIVE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/archive")]
        public async Task<IActionResult> ArchiveReport(int id)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var result = await _reportService.ArchiveReport(userId, id);
                if (result)
                {
                    return Ok(new { message = "Report and related data archived successfully." });
                }
                else
                {
                    return NotFound(new { message = "Report not found." });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        ///             GENERATE USER TOKEN
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        ///             GET USER ID {CLAIMS}
        /// </summary>
        /// <returns></returns>
        private int GetUserIdFromClaims()
        {
            // Extract userId from claims, assuming userId is part of the claims
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        /// <summary>
        ///             GENERATE JWT TOKEN
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJWTToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var secretKey = _configuration["Jwt:Key"]; // Ensure this matches your configuration
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
