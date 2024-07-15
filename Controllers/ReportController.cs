using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IAssociateService _associateService;
        private readonly ICommentService _commentService;

        public ReportController(IReportService reportService, IAssociateService associateService, ICommentService commentService)
        {
            _reportService = reportService;
            _associateService = associateService;
            _commentService = commentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport(Report report)
        {
            var createdReport = await _reportService.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReportById), new { id = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Report>> UpdateReport(int id, Report report)
        {
            if (id != report.ReportId)
            {
                return BadRequest();
            }

            var updatedReport = await _reportService.UpdateReportAsync(report);
            return Ok(updatedReport);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReport(int id)
        {
            var deleted = await _reportService.DeleteReportAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        //[HttpPost("{userId}/track/{reportId}")]
        //public async Task<ActionResult> AddReportToUserTracking(int userId, int reportId)
        //{
        //    var added = await _reportService.AddReportToUserTrackingAsync(userId, reportId);
        //    if (!added)
        //    {
        //        return BadRequest();
        //    }

        //    return NoContent();
        //}

        [HttpGet("{reportId}/associates")]
        public async Task<ActionResult<IEnumerable<Associate>>> GetAssociatesByReportId(int reportId)
        {
            var associates = await _associateService.GetAssociatesByReportIdAsync(reportId);
            return Ok(associates);
        }

        [HttpPost("{reportId}/associates")]
        public async Task<ActionResult<Associate>> AddAssociate(int reportId, Associate associate)
        {
            await _associateService.AddAssociateAsync(reportId, associate);
            return CreatedAtAction(nameof(GetAssociatesByReportId), new { reportId = reportId }, associate);
        }

        [HttpDelete("{reportId}/associates/{associateId}")]
        public async Task<IActionResult> RemoveAssociate(int reportId, int associateId)
        {
            await _associateService.RemoveAssociateAsync(reportId, associateId);
            return NoContent();
        }

        [HttpGet("{reportId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByReportId(int reportId)
        {
            var comments = await _commentService.GetCommentsByReportIdAsync(reportId);
            return Ok(comments);
        }

        [HttpPost("{reportId}/comments")]
        public async Task<ActionResult<Comment>> AddComment(int reportId, Comment comment)
        {
            await _commentService.AddCommentAsync(reportId, comment);
            return CreatedAtAction(nameof(GetCommentsByReportId), new { reportId = reportId }, comment);
        }

        [HttpDelete("{reportId}/comments/{commentId}")]
        public async Task<IActionResult> RemoveComment(int reportId, int commentId)
        {
            await _commentService.RemoveCommentAsync(reportId, commentId);
            return NoContent();
        }
    }

}
