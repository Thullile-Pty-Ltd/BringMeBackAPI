﻿using BringMeBackAPI.Models.Associates;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetAllReports(); // Read access for all users
        Task<Report> GetReportById(int id); // Read access for all users
        Task<Report> CreateReport(int userId, Report report); // Only the user who creates it can update or delete
        Task<Report> UpdateReport(int userId, int id, Report report); // Only the owner can update
        Task<bool> ArchiveReport(int userId, int id); // Only the owner can archive
    }

}
