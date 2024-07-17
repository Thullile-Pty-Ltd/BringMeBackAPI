using BringMeBack.Data;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class MissingPersonReportRepository : IMissingPersonReportRepository
    {
        private readonly ApplicationDbContext _context;

        public MissingPersonReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync()
        {
            return await _context.MissingPersonReports.ToListAsync();
        }

        public async Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId)
        {
            return await _context.MissingPersonReports.FindAsync(reportId);
        }

        public async Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams filterParams)
        {
            var query = _context.MissingPersonReports.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams.FullName))
            {
                query = query.Where(r => r.FullName.Contains(filterParams.FullName));
            }

            if (!string.IsNullOrEmpty(filterParams.LastSeenLocation))
            {
                query = query.Where(r => r.LastSeenLocation.Contains(filterParams.LastSeenLocation));
            }

            if (filterParams.LastSeenDateTimeFrom.HasValue)
            {
                query = query.Where(r => r.LastSeenDateTime >= filterParams.LastSeenDateTimeFrom.Value);
            }

            if (filterParams.LastSeenDateTimeTo.HasValue)
            {
                query = query.Where(r => r.LastSeenDateTime <= filterParams.LastSeenDateTimeTo.Value);
            }

            // Add more filters as required

            return await query.ToListAsync();
        }

        public async Task<object> GetMissingPersonReportsStatisticsAsync()
        {
            var totalReports = await _context.MissingPersonReports.CountAsync();
            var resolvedReports = await _context.MissingPersonReports.CountAsync(r => r.IsResolved);
            var unresolvedReports = totalReports - resolvedReports;

            // Add more statistics as needed

            return new
            {
                TotalReports = totalReports,
                ResolvedReports = resolvedReports,
                UnresolvedReports = unresolvedReports,
                // Additional statistics
            };
        }
    }
}
