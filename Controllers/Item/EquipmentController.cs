using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Services.Reports.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BringMeBackAPI.Controllers.Item
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipmentReport([FromBody] Equipment report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdReport = await _service.CreateEquipmentReport(userId, report);
            return CreatedAtAction(nameof(GetEquipmentReportById), new { reportId = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateEquipmentReport(int reportId, [FromBody] Equipment report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedReport = await _service.UpdateEquipmentReport(userId, reportId, report);
            if (updatedReport == null)
            {
                return Unauthorized();
            }
            return Ok(updatedReport);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllEquipmentReports()
        {
            var reports = await _service.GetAllEquipmentReportsAsync();
            return Ok(reports);
        }

        [AllowAnonymous]
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetEquipmentReportById(int reportId)
        {
            var report = await _service.GetEquipmentReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }

}
