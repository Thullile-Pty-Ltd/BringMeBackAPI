using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers.Dashboards
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoundPersonReportController : ControllerBase
    {
        private readonly IFoundPersonReportService _foundPersonReportService;

        public FoundPersonReportController(IFoundPersonReportService foundPersonReportService)
        {
            _foundPersonReportService = foundPersonReportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundPersonReport>>> GetAllFoundPersonReportsAsync()
        {
            var reports = await _foundPersonReportService.GetAllFoundPersonReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{reportId}")]
        public async Task<ActionResult<FoundPersonReport>> GetFoundPersonReportByIdAsync(int reportId)
        {
            var report = await _foundPersonReportService.GetFoundPersonReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<FoundPersonReport>>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams)
        {
            var filteredReports = await _foundPersonReportService.FilterFoundPersonReportsAsync(filterParams);
            return Ok(filteredReports);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetFoundPersonReportsStatisticsAsync()
        {
            var statistics = await _foundPersonReportService.GetFoundPersonReportsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
