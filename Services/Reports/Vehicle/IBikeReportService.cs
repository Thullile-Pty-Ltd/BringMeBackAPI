using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Services.Reports.Vehicle
{
    public interface IBikeReportService
    {
        Task<Bike> CreateBikeReport(int userId, Bike report);
        Task<Bike> UpdateBikeReport(int userId, int reportId, Bike report);
        Task<IEnumerable<Bike>> GetAllBikeReportsAsync();
        Task<Bike> GetBikeReportByIdAsync(int reportId);
    }

}
