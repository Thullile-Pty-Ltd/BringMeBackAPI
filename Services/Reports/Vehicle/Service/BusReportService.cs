using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Repository.Reports.Vehicle;

namespace BringMeBackAPI.Services.Reports.Vehicle.Service
{
    public class BusReportService : IBusReportService
    {
        private readonly IBusReportRepository _repository;

        public BusReportService(IBusReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Bus> CreateBusReport(int userId, Bus report)
        {
            report.UserId = userId;
            return await _repository.CreateBusReport(report);
        }

        public async Task<Bus> UpdateBusReport(int userId, int reportId, Bus report)
        {
            var existingReport = await _repository.GetBusReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this report.");
            }
            return await _repository.UpdateBusReport(report);
        }

        public async Task<IEnumerable<Bus>> GetAllBusReportsAsync()
        {
            return await _repository.GetAllBusReportsAsync();
        }

        public async Task<Bus> GetBusReportByIdAsync(int reportId)
        {
            return await _repository.GetBusReportByIdAsync(reportId);
        }
    }

}
