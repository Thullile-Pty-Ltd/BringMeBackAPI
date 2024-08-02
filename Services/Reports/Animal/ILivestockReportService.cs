using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Services.Reports.Animal
{
    public interface ILivestockReportService
    {
        Task<Livestock> CreateLivestockReport(int userId, Livestock report);
        Task<Livestock> UpdateLivestockReport(int userId, int reportId, Livestock report);
        Task<IEnumerable<Livestock>> GetAllLivestockReportsAsync();
        Task<Livestock> GetLivestockReportByIdAsync(int reportId);
    }

}
