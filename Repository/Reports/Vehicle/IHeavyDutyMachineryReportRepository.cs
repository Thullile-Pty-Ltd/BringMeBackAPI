using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Repository.Reports.Vehicle
{
    public interface IHeavyDutyMachineryReportRepository
    {
        Task<HeavyDutyMachinery> CreateHeavyDutyMachineryReport(HeavyDutyMachinery report);
        Task<HeavyDutyMachinery> UpdateHeavyDutyMachineryReport(HeavyDutyMachinery report);
        Task<IEnumerable<HeavyDutyMachinery>> GetAllHeavyDutyMachineryReportsAsync();
        Task<HeavyDutyMachinery> GetHeavyDutyMachineryReportByIdAsync(int reportId);
    }

}
