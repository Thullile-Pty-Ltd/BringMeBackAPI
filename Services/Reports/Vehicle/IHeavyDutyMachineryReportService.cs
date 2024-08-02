using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Services.Reports.Vehicle
{
    public interface IHeavyDutyMachineryReportService
    {
        Task<HeavyDutyMachinery> CreateHeavyDutyMachineryReport(int userId, HeavyDutyMachinery report);
        Task<HeavyDutyMachinery> UpdateHeavyDutyMachineryReport(int userId, int reportId, HeavyDutyMachinery report);
        Task<IEnumerable<HeavyDutyMachinery>> GetAllHeavyDutyMachineryReportsAsync();
        Task<HeavyDutyMachinery> GetHeavyDutyMachineryReportByIdAsync(int reportId);
    }

}
