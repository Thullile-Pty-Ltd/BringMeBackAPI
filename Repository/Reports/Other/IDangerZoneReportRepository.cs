using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Models.Reports.others;

namespace BringMeBackAPI.Repository.Reports.Other
{
    public interface IDangerZoneReportRepository
    {
        Task<DangerZone> CreateDangerZoneReport(DangerZone report);
        Task<DangerZone> UpdateDangerZoneReport(DangerZone report);
        Task<IEnumerable<DangerZone>> GetAllDangerZoneReportsAsync();
        Task<DangerZone> GetDangerZoneReportByIdAsync(int reportId);
    }

}
