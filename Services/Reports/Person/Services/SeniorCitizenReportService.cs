using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;

namespace BringMeBackAPI.Services.Reports.Person.Services
{
    public class SeniorCitizenReportService : ISeniorCitizenReportService
    {
        private readonly ISeniorCitizenReportRepository _repository;

        public SeniorCitizenReportService(ISeniorCitizenReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<SeniorCitizen> CreateSeniorCitizenReport(int userId, SeniorCitizen report)
        {
            report.UserId = userId;
            return await _repository.CreateSeniorCitizenReport(report);
        }

        public async Task<SeniorCitizen> UpdateSeniorCitizenReport(int userId, int reportId, SeniorCitizen report)
        {
            var existingReport = await _repository.GetSeniorCitizenReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateSeniorCitizenReport(report);
        }

        public async Task<IEnumerable<SeniorCitizen>> GetAllSeniorCitizenReportsAsync()
        {
            return await _repository.GetAllSeniorCitizenReportsAsync();
        }

        public async Task<SeniorCitizen> GetSeniorCitizenReportByIdAsync(int reportId)
        {
            return await _repository.GetSeniorCitizenReportByIdAsync(reportId);
        }
    }

}
