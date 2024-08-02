using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Services.Reports.Animal
{
    public interface IPetReportService
    {
        Task<Pet> CreatePetReport(int userId, Pet report);
        Task<Pet> UpdatePetReport(int userId, int reportId, Pet report);
        Task<IEnumerable<Pet>> GetAllPetReportsAsync();
        Task<Pet> GetPetReportByIdAsync(int reportId);
    }

}
