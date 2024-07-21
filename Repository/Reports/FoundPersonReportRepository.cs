using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports
{
    public class FoundPersonReportRepository : IFoundPersonReportRepository
    {
        private readonly ApplicationDbContext _context;

        public FoundPersonReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public async Task<FoundPersonReport> CreateFoundPersonReport(FoundPersonReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        /// <summary>
        ///             EDIT / UPDATE
        /// </summary>
        /// <returns></returns>
        public async Task<FoundPersonReport> UpdateFoundPersonReport(FoundPersonReport report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync()
        {
            return await _context.FoundPersonReports.ToListAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId)
        {
            return await _context.FoundPersonReports.FindAsync(reportId);
        }

        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams)
        {
            var query = _context.FoundPersonReports.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.FullName))
            {
                query = query.Where(r => r.FullName.Contains(filterParams.FullName));
            }

            if (!string.IsNullOrEmpty(filterParams.FoundLocation))
            {
                query = query.Where(r => r.FoundLocation.Contains(filterParams.FoundLocation));
            }

            if (filterParams.FoundDateTimeFrom.HasValue)
            {
                query = query.Where(r => r.FoundDateTime >= filterParams.FoundDateTimeFrom.Value);
            }

            if (filterParams.FoundDateTimeTo.HasValue)
            {
                query = query.Where(r => r.FoundDateTime <= filterParams.FoundDateTimeTo.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetFoundPersonReportsStatisticsAsync()
        {
            // Implement logic for statistics
            // Example: return number of reports per month, condition distribution, etc.
            return await Task.FromResult(new
            {
                TotalReports = _context.FoundPersonReports.Count(),
                ConditionDistribution = _context.FoundPersonReports.GroupBy(r => r.ConditionWhenFound).Select(g => new { Condition = g.Key, Count = g.Count() }).ToList()
            });
        }
    }

}
