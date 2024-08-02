using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Repository.Reports.Vehicle
{
    public interface ICarReportRepository
    {
        Task<Car> CreateCarReport(Car report);
        Task<Car> UpdateCarReport(Car report);
        Task<IEnumerable<Car>> GetAllCarReportsAsync();
        Task<Car> GetCarReportByIdAsync(int reportId);
    }

}
