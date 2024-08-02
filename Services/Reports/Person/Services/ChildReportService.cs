using BringMeBackAPI.Models.Reports.Persons;
using BringMeBackAPI.Repository.Reports.Person;

namespace BringMeBackAPI.Services.Reports.Person.Services
{
    public class ChildReportService : IChildReportService
    {
        private readonly IChildReportRepository _repository;

        public ChildReportService(IChildReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Child> CreateChildReport(int userId, Child report)
        {
            if (report.UserId != userId)
            {
                throw new UnauthorizedAccessException("User is not authorized to create this report.");
            }

            return await _repository.CreateChildReport(report);
        }

        public async Task<Child> UpdateChildReport(int userId, int reportId, Child report)
        {
            var existingReport = await _repository.GetChildReportByIdAsync(reportId);

            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("User is not authorized to update this report.");
            }

            report.ReportId = reportId;
            return await _repository.UpdateChildReport(report);
        }

        public async Task<IEnumerable<Child>> GetAllChildReportsAsync()
        {
            return await _repository.GetAllChildReportsAsync();
        }

        public async Task<Child> GetChildReportByIdAsync(int reportId)
        {
            return await _repository.GetChildReportByIdAsync(reportId);
        }
    }

}
