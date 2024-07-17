using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers.Dashboards
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissingItemReportController : ControllerBase
    {
        private readonly IMissingItemReportService _missingItemReportService;

        public MissingItemReportController(IMissingItemReportService missingItemReportService)
        {
            _missingItemReportService = missingItemReportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissingItemReport>>> GetAllMissingItemReportsAsync()
        {
            var reports = await _missingItemReportService.GetAllMissingItemReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{reportId}")]
        public async Task<ActionResult<MissingItemReport>> GetMissingItemReportByIdAsync(int reportId)
        {
            var report = await _missingItemReportService.GetMissingItemReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }

        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<MissingItemReport>>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams)
        {
            var filteredReports = await _missingItemReportService.FilterMissingItemReportsAsync(filterParams);
            return Ok(filteredReports);
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetMissingItemReportsStatisticsAsync()
        {
            var statistics = await _missingItemReportService.GetMissingItemReportsStatisticsAsync();
            return Ok(statistics);
        }
    }
}
