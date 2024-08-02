using BringMeBackAPI.Models.Reports.Vehicles;

namespace BringMeBackAPI.Repository.Reports.Vehicle
{
    public interface IBikeReportRepository
    {
        Task<Bike> CreateBikeReport(Bike report);
        Task<Bike> UpdateBikeReport(Bike report);
        Task<IEnumerable<Bike>> GetAllBikeReportsAsync();
        Task<Bike> GetBikeReportByIdAsync(int reportId);
    }

}
