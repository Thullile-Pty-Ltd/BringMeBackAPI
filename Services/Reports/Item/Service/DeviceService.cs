using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;

namespace BringMeBackAPI.Services.Reports.Item.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;

        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Device> CreateDeviceReport(int userId, Device report)
        {
            report.UserId = userId;
            return await _repository.CreateDeviceReport(report);
        }

        public async Task<Device> UpdateDeviceReport(int userId, int reportId, Device report)
        {
            var existingReport = await _repository.GetDeviceReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle unauthorized access
            }

            report.ReportId = reportId;
            return await _repository.UpdateDeviceReport(report);
        }

        public async Task<IEnumerable<Device>> GetAllDeviceReportsAsync()
        {
            return await _repository.GetAllDeviceReportsAsync();
        }

        public async Task<Device> GetDeviceReportByIdAsync(int reportId)
        {
            return await _repository.GetDeviceReportByIdAsync(reportId);
        }
    }

}
