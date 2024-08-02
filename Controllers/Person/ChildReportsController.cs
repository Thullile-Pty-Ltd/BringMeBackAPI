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
    public class ChildReportsController : ControllerBase
    {
        private readonly IChildReportService _childReportService;
        private readonly IConfiguration _configuration;

        public ChildReportsController(IChildReportService childReportService, IConfiguration configuration)
        {
            _childReportService = childReportService;
            _configuration = configuration;
        }

        /// <summary>
        /// GET ALL CHILD REPORTS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChildReport(int id)
        {
            var report = await _childReportService.GetChildReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        /// <summary>
        /// CREATE CHILD REPORT
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateChildReport([FromBody] Child report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            var createdReport = await _childReportService.CreateChildReport(userId, report);
            return CreatedAtAction(nameof(GetChildReport), new { id = createdReport.ReportId }, createdReport);
        }

        /// <summary>
        /// UPDATE CHILD REPORT
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateChildReport(int id, [FromBody] Child report)
        {
            var userId = GetUserIdFromClaims(); // Extract userId from claims
            try
            {
                var updatedReport = await _childReportService.UpdateChildReport(userId, id, report);
                return Ok(updatedReport);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        ///// <summary>
        ///// DELETE CHILD REPORT
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> DeleteChildReport(int id)
        //{
        //    var userId = GetUserIdFromClaims(); // Extract userId from claims
        //    try
        //    {
        //        var result = await _childReportService.DeleteChildReport(userId, id);
        //        if (result)
        //        {
        //            return NoContent();
        //        }
        //        else
        //        {
        //            return NotFound(new { message = "ChildReport not found." });
        //        }
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Forbid();
        //    }
        //}


        /// <summary>
        /// GET USER ID FROM CLAIMS
        /// </summary>
        /// <returns></returns>
        private int GetUserIdFromClaims()
        {
            // Extract userId from claims, assuming userId is part of the claims
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

       
    }

}
