using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;

namespace BringMeBackAPI.Services.Reports.Person.Services
{
    public class AdultReportService : IAdultReportService
    {
        private readonly IAdultReportRepository _repository;

        public AdultReportService(IAdultReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Adult> CreateAdultReport(int userId, Adult report)
        {
            report.UserId = userId;
            return await _repository.CreateAdultReport(report);
        }

        public async Task<Adult> UpdateAdultReport(int userId, int reportId, Adult report)
        {
            var existingReport = await _repository.GetAdultReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle the case where the report does not exist or the user does not own it
            }

            report.ReportId = reportId;
            return await _repository.UpdateAdultReport(report);
        }

        public async Task<IEnumerable<Adult>> GetAllAdultReportsAsync()
        {
            return await _repository.GetAllAdultReportsAsync();
        }

        public async Task<Adult> GetAdultReportByIdAsync(int reportId)
        {
            return await _repository.GetAdultReportByIdAsync(reportId);
        }
    }

}
