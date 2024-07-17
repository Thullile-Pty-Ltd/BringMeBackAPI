using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers.Dashboards
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingPersonReportController : ControllerBase
    {
        private readonly IMissingPersonReportService _missingPersonReportService;

        public MissingPersonReportController(IMissingPersonReportService missingPersonReportService)
        {
            _missingPersonReportService = missingPersonReportService;
        }

        // GET: api/MissingPersonReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissingPersonReport>>> GetAllReports()
        {
            var reports = await _missingPersonReportService.GetAllMissingPersonReportsAsync();
            return Ok(reports);
        }

        // GET: api/MissingPersonReport/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MissingPersonReport>> GetReportById(int id)
        {
            var report = await _missingPersonReportService.GetMissingPersonReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        // POST: api/MissingPersonReport/filter
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<MissingPersonReport>>> FilterReports([FromBody] MissingPersonReportFilterParams filterParams)
        {
            var reports = await _missingPersonReportService.FilterMissingPersonReportsAsync(filterParams);
            return Ok(reports);
        }

        // GET: api/MissingPersonReport/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetStatistics()
        {
            var stats = await _missingPersonReportService.GetMissingPersonReportsStatisticsAsync();
            return Ok(stats);
        }
    }
}
