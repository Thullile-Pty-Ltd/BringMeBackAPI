using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Repository.Reports.Animal
{
    public interface IWildReportRepository
    {
        Task<Wild> CreateWildReport(Wild report);
        Task<Wild> UpdateWildReport(Wild report);
        Task<IEnumerable<Wild>> GetAllWildReportsAsync();
        Task<Wild> GetWildReportByIdAsync(int reportId);
    }

}
