﻿using BringMeBackAPI.Models.Reports.Persons;
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
    public class SeniorCitizenReportsController : ControllerBase
    {
        private readonly ISeniorCitizenReportService _seniorCitizenReportService;
        private readonly IConfiguration _configuration;

        public SeniorCitizenReportsController(ISeniorCitizenReportService seniorCitizenReportService, IConfiguration configuration)
        {
            _seniorCitizenReportService = seniorCitizenReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeniorCitizenReport(int id)
        {
            var report = await _seniorCitizenReportService.GetSeniorCitizenReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSeniorCitizenReport([FromBody] SeniorCitizen report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _seniorCitizenReportService.CreateSeniorCitizenReport(userId, report);
            return CreatedAtAction(nameof(GetSeniorCitizenReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateSeniorCitizenReport(int id, [FromBody] SeniorCitizen report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _seniorCitizenReportService.UpdateSeniorCitizenReport(userId, id, report);
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
