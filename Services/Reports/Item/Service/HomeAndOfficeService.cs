using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;

namespace BringMeBackAPI.Services.Reports.Item.Service
{
    public class HomeAndOfficeService : IHomeAndOfficeService
    {
        private readonly IHomeAndOfficeRepository _repository;

        public HomeAndOfficeService(IHomeAndOfficeRepository repository)
        {
            _repository = repository;
        }

        public async Task<HomeAndOffice> CreateHomeAndOfficeReport(int userId, HomeAndOffice report)
        {
            report.UserId = userId;
            return await _repository.CreateHomeAndOfficeReport(report);
        }

        public async Task<HomeAndOffice> UpdateHomeAndOfficeReport(int userId, int reportId, HomeAndOffice report)
        {
            var existingReport = await _repository.GetHomeAndOfficeReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle unauthorized access
            }

            report.ReportId = reportId;
            return await _repository.UpdateHomeAndOfficeReport(report);
        }

        public async Task<IEnumerable<HomeAndOffice>> GetAllHomeAndOfficeReportsAsync()
        {
            return await _repository.GetAllHomeAndOfficeReportsAsync();
        }

        public async Task<HomeAndOffice> GetHomeAndOfficeReportByIdAsync(int reportId)
        {
            return await _repository.GetHomeAndOfficeReportByIdAsync(reportId);
        }
    }

}
