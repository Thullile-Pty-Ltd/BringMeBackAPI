using BringMeBackAPI.Models.Reports.Animals;

namespace BringMeBackAPI.Repository.Reports.Animal
{
    public interface IPetReportRepository
    {
        Task<Pet> CreatePetReport(Pet report);
        Task<Pet> UpdatePetReport(Pet report);
        Task<IEnumerable<Pet>> GetAllPetReportsAsync();
        Task<Pet> GetPetReportByIdAsync(int reportId);
    }

}
