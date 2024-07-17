using BringMeBack.Data;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports
{
    public class FoundItemReportRepository : IFoundItemReportRepository
    {
        private readonly ApplicationDbContext _context;

        public FoundItemReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync()
        {
            return await _context.FoundItemReports.ToListAsync();
        }

        public async Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId)
        {
            return await _context.FoundItemReports.FindAsync(reportId);
        }

        public async Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams)
        {
            var query = _context.FoundItemReports.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.ItemName))
            {
                query = query.Where(r => r.ItemName.Contains(filterParams.ItemName));
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

        public async Task<object> GetFoundItemReportsStatisticsAsync()
        {
            // Implement logic for statistics
            // Example: return number of reports per month, condition distribution, etc.
            return await Task.FromResult(new
            {
                TotalReports = _context.FoundItemReports.Count(),
                ConditionDistribution = _context.FoundItemReports.GroupBy(r => r.ConditionOfItemWhenFound).Select(g => new { Condition = g.Key, Count = g.Count() }).ToList()
            });
        }
    }
}
