using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;

namespace BringMeBackAPI.Services.Reports.Person.Services
{
    public class WantedPersonReportService : IWantedPersonReportService
    {
        private readonly IWantedPersonReportRepository _repository;

        public WantedPersonReportService(IWantedPersonReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<WantedPerson> CreateWantedPersonReport(int userId, WantedPerson report)
        {
            report.UserId = userId;
            return await _repository.CreateWantedPersonReport(report);
        }

        public async Task<WantedPerson> UpdateWantedPersonReport(int userId, int reportId, WantedPerson report)
        {
            var existingReport = await _repository.GetWantedPersonReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateWantedPersonReport(report);
        }

        public async Task<IEnumerable<WantedPerson>> GetAllWantedPersonReportsAsync()
        {
            return await _repository.GetAllWantedPersonReportsAsync();
        }

        public async Task<WantedPerson> GetWantedPersonReportByIdAsync(int reportId)
        {
            return await _repository.GetWantedPersonReportByIdAsync(reportId);
        }
    }

}
