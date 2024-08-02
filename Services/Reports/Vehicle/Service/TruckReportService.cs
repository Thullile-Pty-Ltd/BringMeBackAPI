using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Repository.Reports.Vehicle;

namespace BringMeBackAPI.Services.Reports.Vehicle.Service
{
    public class TruckReportService : ITruckReportService
    {
        private readonly ITruckReportRepository _repository;

        public TruckReportService(ITruckReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Truck> CreateTruckReport(int userId, Truck report)
        {
            report.UserId = userId;
            return await _repository.CreateTruckReport(report);
        }

        public async Task<Truck> UpdateTruckReport(int userId, int reportId, Truck report)
        {
            var existingReport = await _repository.GetTruckReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this report.");
            }
            return await _repository.UpdateTruckReport(report);
        }

        public async Task<IEnumerable<Truck>> GetAllTruckReportsAsync()
        {
            return await _repository.GetAllTruckReportsAsync();
        }

        public async Task<Truck> GetTruckReportByIdAsync(int reportId)
        {
            return await _repository.GetTruckReportByIdAsync(reportId);
        }
    }

}
