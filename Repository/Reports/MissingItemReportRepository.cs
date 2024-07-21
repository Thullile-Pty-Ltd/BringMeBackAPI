using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using Microsoft.EntityFrameworkCore;
using BringMeBackAPI.Repository.Reports.Interfaces;

namespace BringMeBackAPI.Repository.Reports
{
    public class MissingItemReportRepository : IMissingItemReportRepository
    {
        private readonly ApplicationDbContext _context;

        public MissingItemReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public async Task<MissingItemReport> CreateMissingItemReport(MissingItemReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        /// <summary>
        ///             EDIT / UPDATE
        /// </summary>
        /// <returns></returns>
        public async Task<MissingItemReport> UpdateMissingItemReport(MissingItemReport report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync()
        {
            return await _context.MissingItemReports.ToListAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId)
        {
            return await _context.MissingItemReports.FindAsync(reportId);
        }

        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams)
        {
            var query = _context.MissingItemReports.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.ItemName))
            {
                query = query.Where(r => r.ItemName.Contains(filterParams.ItemName));
            }

            if (!string.IsNullOrEmpty(filterParams.LastKnownLocation))
            {
                query = query.Where(r => r.LastKnownLocation.Contains(filterParams.LastKnownLocation));
            }

            if (filterParams.LastSeenDateTimeFrom.HasValue)
            {
                query = query.Where(r => r.LastSeenDateTime >= filterParams.LastSeenDateTimeFrom.Value);
            }

            if (filterParams.LastSeenDateTimeTo.HasValue)
            {
                query = query.Where(r => r.LastSeenDateTime <= filterParams.LastSeenDateTimeTo.Value);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetMissingItemReportsStatisticsAsync()
        {
            // Implement logic for statistics
            // Example: return number of reports per item type, average reward offered, etc.
            return await Task.FromResult(new
            {
                TotalReports = _context.MissingItemReports.Count(),
                AverageRewardOffered = _context.MissingItemReports.Average(r => r.RewardOffered ?? 0)
            });
        }
    }
}
