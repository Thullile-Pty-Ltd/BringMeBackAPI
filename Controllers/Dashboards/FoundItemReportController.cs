using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers.Dashboards
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoundItemReportController : ControllerBase
    {
        private readonly IFoundItemReportService _foundItemReportService;

        public FoundItemReportController(IFoundItemReportService foundItemReportService)
        {
            _foundItemReportService = foundItemReportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundItemReport>>> GetAllFoundItemReportsAsync()
        {
            var reports = await _foundItemReportService.GetAllFoundItemReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{reportId}")]
        public async Task<ActionResult<FoundItemReport>> GetFoundItemReportByIdAsync(int reportId)
        {
            var report = await _foundItemReportService.GetFoundItemReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<FoundItemReport>>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams)
        {
            var filteredReports = await _foundItemReportService.FilterFoundItemReportsAsync(filterParams);
            return Ok(filteredReports);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetFoundItemReportsStatisticsAsync()
        {
            var statistics = await _foundItemReportService.GetFoundItemReportsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
