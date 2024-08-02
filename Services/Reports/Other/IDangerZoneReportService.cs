using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;

namespace BringMeBackAPI.Services.Reports.Other
{
    public interface IDangerZoneReportService
    {
        Task<DangerZone> CreateDangerZoneReport(int userId, DangerZone report);
        Task<DangerZone> UpdateDangerZoneReport(int userId, int reportId, DangerZone report);
        Task<IEnumerable<DangerZone>> GetAllDangerZoneReportsAsync();
        Task<DangerZone> GetDangerZoneReportByIdAsync(int reportId);
    }
}
