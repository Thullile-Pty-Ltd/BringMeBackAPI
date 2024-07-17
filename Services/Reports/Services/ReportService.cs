using BringMeBack.Data;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Services.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Services.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.Include(r => r.Associates).Include(r => r.Comments).ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(int reportId)
        {
            return await _context.Reports.Include(r => r.Associates).Include(r => r.Comments).FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        public async Task<Report> CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
            {
                return false;
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MissingPersonReport>> GetAllPersonReportsAsync()
        {
            return await _context.MissingPersonReports.ToListAsync();
        }

        public async Task<IEnumerable<MissingItemReport>> GetAllItemReportsAsync()
        {
            return await _context.MissingItemReports.ToListAsync();
        }

        public async Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync()
        {
            return await _context.FoundPersonReports.ToListAsync();
        }

        public async Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync()
        {
            return await _context.FoundItemReports.ToListAsync();
        }

        //public async Task<bool> AddReportToUserTrackingAsync(int userId, int reportId)
        //{
        //    var user = await _context.Users.Include(u => u.TrackedReports).FirstOrDefaultAsync(u => u.UserId == userId);
        //    if (user == null || user.TrackedReports.Any(r => r.ReportId == reportId))
        //    {
        //        return false;
        //    }

        //    var report = await _context.Reports.FindAsync(reportId);
        //    if (report == null)
        //    {
        //        return false;
        //    }

        //    user.TrackedReports.Add(report);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }

    public class AssociateService : IAssociateService
    {
        private readonly ApplicationDbContext _context;

        public AssociateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Associate>> GetAssociatesByReportIdAsync(int reportId)
        {
            var report = await _context.Reports
                .Include(r => r.Associates)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            return report?.Associates;
        }

        public async Task AddAssociateAsync(int reportId, Associate associate)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                throw new KeyNotFoundException("Report not found");

            if (report.Associates == null)
                report.Associates = new List<Associate>();

            report.Associates.Add(associate);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAssociateAsync(int reportId, int associateId)
        {
            var report = await _context.Reports
                .Include(r => r.Associates)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            if (report == null)
                throw new KeyNotFoundException("Report not found");

            var associate = report.Associates.FirstOrDefault(a => a.AssociateId == associateId);
            if (associate != null)
            {
                report.Associates.Remove(associate);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByReportIdAsync(int reportId)
        {
            var report = await _context.Reports
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            return report?.Comments;
        }

        public async Task AddCommentAsync(int reportId, Comment comment)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                throw new KeyNotFoundException("Report not found");

            if (report.Comments == null)
                report.Comments = new List<Comment>();

            report.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCommentAsync(int reportId, int commentId)
        {
            var report = await _context.Reports
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.ReportId == reportId);

            if (report == null)
                throw new KeyNotFoundException("Report not found");

            var comment = report.Comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                report.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
