using BringMeBackAPI.Models.Reports.Animals;
using BringMeBackAPI.Repository.Reports.Animal;

namespace BringMeBackAPI.Services.Reports.Animal.Service
{
    public class PetReportService : IPetReportService
    {
        private readonly IPetReportRepository _repository;

        public PetReportService(IPetReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pet> CreatePetReport(int userId, Pet report)
        {
            report.UserId = userId;
            return await _repository.CreatePetReport(report);
        }

        public async Task<Pet> UpdatePetReport(int userId, int reportId, Pet report)
        {
            var existingReport = await _repository.GetPetReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdatePetReport(report);
        }

        public async Task<IEnumerable<Pet>> GetAllPetReportsAsync()
        {
            return await _repository.GetAllPetReportsAsync();
        }

        public async Task<Pet> GetPetReportByIdAsync(int reportId)
        {
            return await _repository.GetPetReportByIdAsync(reportId);
        }
    }

}
