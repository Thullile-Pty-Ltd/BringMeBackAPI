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
    public class HomeAndOfficeController : ControllerBase
    {
        private readonly IHomeAndOfficeService _service;

        public HomeAndOfficeController(IHomeAndOfficeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomeAndOfficeReport([FromBody] HomeAndOffice report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdReport = await _service.CreateHomeAndOfficeReport(userId, report);
            return CreatedAtAction(nameof(GetHomeAndOfficeReportById), new { reportId = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateHomeAndOfficeReport(int reportId, [FromBody] HomeAndOffice report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedReport = await _service.UpdateHomeAndOfficeReport(userId, reportId, report);
            if (updatedReport == null)
            {
                return Unauthorized();
            }
            return Ok(updatedReport);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllHomeAndOfficeReports()
        {
            var reports = await _service.GetAllHomeAndOfficeReportsAsync();
            return Ok(reports);
        }

        [AllowAnonymous]
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetHomeAndOfficeReportById(int reportId)
        {
            var report = await _service.GetHomeAndOfficeReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }

}
