using BringMeBack.Data;
using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Interfaces;
using BringMeBackAPI.Services.Users.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace BringMeBackAPI.Services.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserService _userService;

        public ReportService(IReportRepository reportRepository, IUserService userService)
        {
            _reportRepository = reportRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllReports();
        }

        public async Task<Report> GetReportById(int id)
        {
            return await _reportRepository.GetReportById(id);
        }

        public async Task<Report> CreateReport(int userId, Report report)
        {
            // Ensure the UserId in the report matches the logged-in user
            report.UserId = userId;

            // Fetch the user details from the database
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Populate the flattened user data in the report
            report.UserId = user.Id;
            report.UserName = user.Name;
            report.UserEmail = user.Email;
            report.PhoneNumber = user.PhoneNumber;
            report.Location = user.Location;
            report.Role = user.Role;

            // Set other report properties as needed
            report.CreatedAt = DateTime.UtcNow;
            return await _reportRepository.CreateReport(report);
        }

        public async Task<Report> UpdateReport(int userId, int id, Report report)
        {
            var existingReport = await _reportRepository.GetReportById(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this report.");
            }

            // Update the report details
            existingReport.Description = report.Description;
            existingReport.ReportType = report.ReportType;
            existingReport.IsResolved = report.IsResolved;
            existingReport.IsArchived = report.IsArchived;

            return await _reportRepository.UpdateReport(existingReport);
        }

        public async Task<bool> ArchiveReport(int userId, int id)
        {
            var existingReport = await _reportRepository.GetReportById(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to archive this report.");
            }

            return await _reportRepository.ArchiveReport(id);
        }

        // Comment related methods
        public async Task<List<ParentComment>> GetParentCommentsByReportId(int reportId)
        {
            return await _reportRepository.GetParentCommentsByReportId(reportId);
        }

        public async Task<ParentComment> AddParentComment(int userId, int reportId, ParentComment comment)
        {
            // Additional logic to handle userId and reportId if needed
            comment.ReportId = reportId; // Ensure the report ID is set
            return await _reportRepository.AddParentComment(comment);
        }

        public async Task<ParentComment> GetParentCommentById(int commentId)
        {
            return await _reportRepository.GetParentCommentById(commentId);
        }

        public async Task<bool> DeleteParentComment(int userId, int commentId)
        {
            // Additional logic to handle userId if needed
            return await _reportRepository.DeleteParentComment(commentId);
        }

        public async Task<List<ReplyComment>> GetRepliesByParentCommentId(int parentCommentId)
        {
            return await _reportRepository.GetRepliesByParentCommentId(parentCommentId);
        }

        public async Task<ReplyComment> AddReplyComment(int userId, int parentCommentId, ReplyComment reply)
        {
            // Additional logic to handle userId and parentCommentId if needed
            reply.ParentCommentId = parentCommentId; // Ensure the parent comment ID is set
            return await _reportRepository.AddReplyComment(reply);
        }

        public async Task<ReplyComment> GetReplyCommentById(int commentId)
        {
            return await _reportRepository.GetReplyCommentById(commentId);
        }

        public async Task<bool> DeleteReplyComment(int userId, int commentId)
        {
            // Additional logic to handle userId if needed
            return await _reportRepository.DeleteReplyComment(commentId);
        }
    }

}
