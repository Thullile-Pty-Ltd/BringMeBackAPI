using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;

namespace BringMeBackAPI.Repository.Reports.Other
{
    public interface IAnnouncementReportRepository
    {
        Task<Announcement> CreateAnnouncementReport(Announcement report);
        Task<Announcement> UpdateAnnouncementReport(Announcement report);
        Task<IEnumerable<Announcement>> GetAllAnnouncementReportsAsync();
        Task<Announcement> GetAnnouncementReportByIdAsync(int reportId);
    }

}
