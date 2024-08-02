using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;

namespace BringMeBackAPI.Services.Reports.Other
{
    public interface IAnnouncementReportService
    {
        Task<Announcement> CreateAnnouncementReport(int userId, Announcement report);
        Task<Announcement> UpdateAnnouncementReport(int userId, int reportId, Announcement report);
        Task<IEnumerable<Announcement>> GetAllAnnouncementReportsAsync();
        Task<Announcement> GetAnnouncementReportByIdAsync(int reportId);
    }
}
