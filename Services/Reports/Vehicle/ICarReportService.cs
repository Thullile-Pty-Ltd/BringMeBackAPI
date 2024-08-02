using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Services.Reports.Vehicle
{
    public interface ICarReportService
    {
        Task<Car> CreateCarReport(int userId, Car report);
        Task<Car> UpdateCarReport(int userId, int reportId, Car report);
        Task<IEnumerable<Car>> GetAllCarReportsAsync();
        Task<Car> GetCarReportByIdAsync(int reportId);
    }

}
