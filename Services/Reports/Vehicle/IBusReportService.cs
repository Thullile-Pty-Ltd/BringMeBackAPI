using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Services.Reports.Vehicle
{
    public interface IBusReportService
    {
        Task<Bus> CreateBusReport(int userId, Bus report);
        Task<Bus> UpdateBusReport(int userId, int reportId, Bus report);
        Task<IEnumerable<Bus>> GetAllBusReportsAsync();
        Task<Bus> GetBusReportByIdAsync(int reportId);
    }

}
