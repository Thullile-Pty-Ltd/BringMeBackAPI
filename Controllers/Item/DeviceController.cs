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
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DeviceController(IDeviceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceReport([FromBody] Device report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdReport = await _service.CreateDeviceReport(userId, report);
            return CreatedAtAction(nameof(GetDeviceReportById), new { reportId = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateDeviceReport(int reportId, [FromBody] Device report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedReport = await _service.UpdateDeviceReport(userId, reportId, report);
            if (updatedReport == null)
            {
                return Unauthorized();
            }
            return Ok(updatedReport);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllDeviceReports()
        {
            var reports = await _service.GetAllDeviceReportsAsync();
            return Ok(reports);
        }

        [AllowAnonymous]
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetDeviceReportById(int reportId)
        {
            var report = await _service.GetDeviceReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }

}
