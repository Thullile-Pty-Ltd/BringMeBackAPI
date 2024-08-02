﻿using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Services.Reports.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BringMeBackAPI.Controllers.Vehicle
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TruckReportsController : ControllerBase
    {
        private readonly ITruckReportService _truckReportService;
        private readonly IConfiguration _configuration;

        public TruckReportsController(ITruckReportService truckReportService, IConfiguration configuration)
        {
            _truckReportService = truckReportService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTruckReport(int id)
        {
            var report = await _truckReportService.GetTruckReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTruckReport([FromBody] Truck report)
        {
            var userId = GetUserIdFromClaims();
            var createdReport = await _truckReportService.CreateTruckReport(userId, report);
            return CreatedAtAction(nameof(GetTruckReport), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTruckReport(int id, [FromBody] Truck report)
        {
            var userId = GetUserIdFromClaims();
            try
            {
                var updatedReport = await _truckReportService.UpdateTruckReport(userId, id, report);
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