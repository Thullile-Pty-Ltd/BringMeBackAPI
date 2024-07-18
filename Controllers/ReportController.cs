using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _service.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(int id)
        {
            var report = await _service.GetReportById(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] Report report)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdReport = await _service.CreateReport(report);
            return CreatedAtAction(nameof(GetReportById), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] Report report)
        {
            if (id != report.ReportId) return BadRequest("ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updatedReport = await _service.UpdateReport(id, report);
                return Ok(updatedReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ArchiveReport(int id)
        {
            var success = await _service.ArchiveReport(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }

}
