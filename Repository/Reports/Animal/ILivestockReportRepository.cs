using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Repository.Reports.Animal
{
    public interface ILivestockReportRepository
    {
        Task<Livestock> CreateLivestockReport(Livestock report);
        Task<Livestock> UpdateLivestockReport(Livestock report);
        Task<IEnumerable<Livestock>> GetAllLivestockReportsAsync();
        Task<Livestock> GetLivestockReportByIdAsync(int reportId);
    }

}
