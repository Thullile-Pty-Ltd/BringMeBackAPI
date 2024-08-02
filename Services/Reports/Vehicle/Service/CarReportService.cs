using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Repository.Reports.Vehicle;

namespace BringMeBackAPI.Services.Reports.Vehicle.Service
{
    public class CarReportService : ICarReportService
    {
        private readonly ICarReportRepository _repository;

        public CarReportService(ICarReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Car> CreateCarReport(int userId, Car report)
        {
            report.UserId = userId;
            return await _repository.CreateCarReport(report);
        }

        public async Task<Car> UpdateCarReport(int userId, int reportId, Car report)
        {
            var existingReport = await _repository.GetCarReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this report.");
            }
            return await _repository.UpdateCarReport(report);
        }

        public async Task<IEnumerable<Car>> GetAllCarReportsAsync()
        {
            return await _repository.GetAllCarReportsAsync();
        }

        public async Task<Car> GetCarReportByIdAsync(int reportId)
        {
            return await _repository.GetCarReportByIdAsync(reportId);
        }
    }

}
