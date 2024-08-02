using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Repository.Reports.Vehicle
{
    public interface IBusReportRepository
    {
        Task<Bus> CreateBusReport(Bus report);
        Task<Bus> UpdateBusReport(Bus report);
        Task<IEnumerable<Bus>> GetAllBusReportsAsync();
        Task<Bus> GetBusReportByIdAsync(int reportId);
    }

}
