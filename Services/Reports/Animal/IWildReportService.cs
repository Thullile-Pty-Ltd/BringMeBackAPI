using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Services.Reports.Animal
{
    public interface IWildReportService
    {
        Task<Wild> CreateWildReport(int userId, Wild report);
        Task<Wild> UpdateWildReport(int userId, int reportId, Wild report);
        Task<IEnumerable<Wild>> GetAllWildReportsAsync();
        Task<Wild> GetWildReportByIdAsync(int reportId);
    }

}
