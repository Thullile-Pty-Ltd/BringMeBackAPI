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
    public class ClothingAndAccessoriesController : ControllerBase
    {
        private readonly IClothingAndAccessoriesService _service;

        public ClothingAndAccessoriesController(IClothingAndAccessoriesService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClothingAndAccessoriesReport([FromBody] ClothingAndAccessories report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdReport = await _service.CreateClothingAndAccessoriesReport(userId, report);
            return CreatedAtAction(nameof(GetClothingAndAccessoriesReportById), new { reportId = createdReport.ReportId }, createdReport);
        }

        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateClothingAndAccessoriesReport(int reportId, [FromBody] ClothingAndAccessories report)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedReport = await _service.UpdateClothingAndAccessoriesReport(userId, reportId, report);
            if (updatedReport == null)
            {
                return Unauthorized();
            }
            return Ok(updatedReport);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllClothingAndAccessoriesReports()
        {
            var reports = await _service.GetAllClothingAndAccessoriesReportsAsync();
            return Ok(reports);
        }

        [AllowAnonymous]
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetClothingAndAccessoriesReportById(int reportId)
        {
            var report = await _service.GetClothingAndAccessoriesReportByIdAsync(reportId);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }

}
