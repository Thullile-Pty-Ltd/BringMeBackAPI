using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Services.Reports.Vehicle
{
    public interface ITruckReportService
    {
        Task<Truck> CreateTruckReport(int userId, Truck report);
        Task<Truck> UpdateTruckReport(int userId, int reportId, Truck report);
        Task<IEnumerable<Truck>> GetAllTruckReportsAsync();
        Task<Truck> GetTruckReportByIdAsync(int reportId);
    }

}
