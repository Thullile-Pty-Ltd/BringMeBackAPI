using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Repository.Reports.Vehicle
{
    public interface ITruckReportRepository
    {
        Task<Truck> CreateTruckReport(Truck report);
        Task<Truck> UpdateTruckReport(Truck report);
        Task<IEnumerable<Truck>> GetAllTruckReportsAsync();
        Task<Truck> GetTruckReportByIdAsync(int reportId);
    }

}
