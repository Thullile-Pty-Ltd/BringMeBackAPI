using BringMeBack.Data;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Repository.Reports
{
    using BringMeBackAPI.Models.Comments;
    using BringMeBackAPI.Models.Reports;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            // Return all reports. Adjust as needed based on your requirements.
            return await _context.Reports
                .Include(r => r.Associates)
                .Include(r => r.Comments)
                .ToListAsync();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _context.Reports
                .Include(r => r.Associates)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.ReportId == id);
        }

        public async Task<Report> CreateReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
       
        public async Task<FoundPersonReport> CreateFoundPersonReport(FoundPersonReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
        public async Task<MissingItemReport> CreateMissingItemReport(MissingItemReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }
        public async Task<FoundItemReport> CreateFoundItemReport(FoundItemReport report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> UpdateReport(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> ArchiveReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null) return false;

            report.IsArchived = true;
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return true;
        }
        // Comment related methods
        public async Task<List<ParentComment>> GetParentCommentsByReportId(int reportId)
        {
            return await _context.ParentComments
                .Where(c => c.ReportId == reportId)
                .ToListAsync();
        }

        public async Task<ParentComment> AddParentComment(ParentComment comment)
        {
            _context.ParentComments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<ParentComment> GetParentCommentById(int commentId)
        {
            return await _context.ParentComments.FindAsync(commentId);
        }

        public async Task<bool> DeleteParentComment(int commentId)
        {
            var comment = await _context.ParentComments.FindAsync(commentId);
            if (comment == null) return false;

            _context.ParentComments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReplyComment>> GetRepliesByParentCommentId(int parentCommentId)
        {
            return await _context.ReplyComments
                .Where(r => r.ParentCommentId == parentCommentId)
                .ToListAsync();
        }

        public async Task<ReplyComment> AddReplyComment(ReplyComment reply)
        {
            _context.ReplyComments.Add(reply);
            await _context.SaveChangesAsync();
            return reply;
        }

        public async Task<ReplyComment> GetReplyCommentById(int commentId)
        {
            return await _context.ReplyComments.FindAsync(commentId);
        }

        public async Task<bool> DeleteReplyComment(int commentId)
        {
            var reply = await _context.ReplyComments.FindAsync(commentId);
            if (reply == null) return false;

            _context.ReplyComments.Remove(reply);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
