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
using BringMeBackAPI.Services.Reports.Services;
using BringMeBackAPI.Models.Comments;

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
        private readonly IReportMatchingService _reportMatchingService;


        public ReportsController(IReportService reportService, IConfiguration configuration, 
            IMissingPersonReportService missingPersonReportService, 
            IMissingItemReportService missingItemReportService, 
            IFoundPersonReportService foundPersonReportService,
            IFoundItemReportService foundItemReportService, IReportMatchingService reportMatchingService)
        {
            _reportService = reportService;
            _configuration = configuration;
            _missingPersonReportService = missingPersonReportService;
            _missingItemReportService = missingItemReportService;
            _foundPersonReportService = foundPersonReportService;
            _foundItemReportService = foundItemReportService;
            _reportMatchingService = reportMatchingService;
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
        ///             CAMPARE MISSING PERSON
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        [HttpPost("compare-missing-person-report")]
        public async Task<IActionResult> CompareMissingPersonReport([FromBody] MissingPersonReport newReport)
        {

            // Ensure that the new report can be compared with both missing and found person reports
            var (isMatch, matchedReport, matchPercentage) = await _reportMatchingService.FindMissingPersonMatchingReportAsync(newReport);

            if (isMatch)
            {
                // Return success response with match details
                return Ok(new { match = true, matchPercentage, matchedReport });
            }

            // Return response indicating no match found
            return Ok(new { match = false, matchPercentage });
        }

        /// <summary>
        ///             CAMPARE FOUND PERSON
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        [HttpPost("compare-found-person-report")]
        public async Task<IActionResult> CompareFoundPersonReport([FromBody] FoundPersonReport newReport)
        {

            // Ensure that the new report can be compared with both missing and found person reports
            var (isMatch, matchedReport, matchPercentage) = await _reportMatchingService.FindFoundPersonMatchingReportAsync(newReport);

            if (isMatch)
            {
                // Return success response with match details
                return Ok(new { match = true, matchPercentage, matchedReport });
            }

            // Return response indicating no match found
            return Ok(new { match = false, matchPercentage });
        }

        /// <summary>
        ///             CAMPARE MISSING ITEM
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        [HttpPost("compare-missing-item-report")]
        public async Task<IActionResult> CompareMissingItemReport([FromBody] MissingItemReport newReport)
        {

            // Ensure that the new report can be compared with both missing and found person reports
            var (isMatch, matchedReport, matchPercentage) = await _reportMatchingService.FindMissingItemMatchingReportAsync(newReport);

            if (isMatch)
            {
                // Return success response with match details
                return Ok(new { match = true, matchPercentage, matchedReport });
            }

            // Return response indicating no match found
            return Ok(new { match = false, matchPercentage });
        }

        /// <summary>
        ///             CAMPARE FOUND ITEM
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        [HttpPost("compare-found-item-report")]
        public async Task<IActionResult> CompareFoundItemReport([FromBody] FoundItemReport newReport)
        {

            // Ensure that the new report can be compared with both missing and found person reports
            var (isMatch, matchedReport, matchPercentage) = await _reportMatchingService.FindFoundItemMatchingReportAsync(newReport);

            if (isMatch)
            {
                // Return success response with match details
                return Ok(new { match = true, matchPercentage, matchedReport });
            }

            // Return response indicating no match found
            return Ok(new { match = false, matchPercentage });
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

        // Comment Endpoints
        // Get all comments for a specific report

        [HttpGet("get/{reportId}/parent-comments")]
        public async Task<IActionResult> GetParentCommentsByReportId(int reportId)
        {
            var comments = await _reportService.GetParentCommentsByReportId(reportId);
            if (comments == null || !comments.Any())
            {
                return NoContent();
            }
            return Ok(comments);
        }

        [HttpPost("{reportId}/add/parent-comments")]
        public async Task<IActionResult> AddParentComment(int reportId, [FromBody] ParentComment comment)
        {
            var report = await _reportService.GetReportById(reportId);
            if (report == null)
            {
                return NotFound("Report not found");
            }

            comment.Report = report;
            comment.ReportId = reportId;
            comment.CreatedAt = DateTime.UtcNow;
            var userId = GetUserIdFromClaims();
            comment.UserId = userId;

            var createdComment = await _reportService.AddParentComment(userId, reportId, comment);
            return CreatedAtAction(nameof(GetParentCommentById), new { commentId = createdComment.CommentId }, createdComment);
        }

        [HttpGet("get/parent-comments/{commentId}")]
        public async Task<IActionResult> GetParentCommentById(int commentId)
        {
            var comment = await _reportService.GetParentCommentById(commentId);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpDelete("delete/parent-comments/{commentId}")]
        public async Task<IActionResult> DeleteParentComment(int commentId)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var result = await _reportService.DeleteParentComment(userId, commentId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("parent-comments/get/{parentCommentId}/replies")]
        public async Task<IActionResult> GetRepliesByParentCommentId(int parentCommentId)
        {
            var replies = await _reportService.GetRepliesByParentCommentId(parentCommentId);
            if (replies == null || !replies.Any())
            {
                return NoContent();
            }
            return Ok(replies);
        }

        [HttpPost("parent-comments/{parentCommentId}/add/replies")]
        public async Task<IActionResult> AddReplyComment(int parentCommentId, [FromBody] ReplyComment reply)
        {
            var parentComment = await _reportService.GetParentCommentById(parentCommentId);
            if (parentComment == null)
            {
                return NotFound("Parent comment not found");
            }
           

            reply.ParentComment = parentComment;
            reply.ParentCommentId = parentCommentId;           
            reply.CreatedAt = DateTime.UtcNow;
            var userId = GetUserIdFromClaims();
            reply.UserId = userId;

            var createdReply = await _reportService.AddReplyComment(userId, parentCommentId, reply);
            return CreatedAtAction(nameof(GetReplyCommentById), new { commentId = createdReply.CommentId }, createdReply);
        }

        [HttpGet("get/replies/{commentId}")]
        public async Task<IActionResult> GetReplyCommentById(int commentId)
        {
            var comment = await _reportService.GetReplyCommentById(commentId);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpDelete("delete/replies/{commentId}")]
        public async Task<IActionResult> DeleteReplyComment(int commentId)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var result = await _reportService.DeleteReplyComment(userId, commentId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
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
