using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Repository.Reports.Vehicle;

namespace BringMeBackAPI.Services.Reports.Vehicle.Service
{
    public class BikeReportService : IBikeReportService
    {
        private readonly IBikeReportRepository _repository;

        public BikeReportService(IBikeReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Bike> CreateBikeReport(int userId, Bike report)
        {
            report.UserId = userId;
            return await _repository.CreateBikeReport(report);
        }

        public async Task<Bike> UpdateBikeReport(int userId, int reportId, Bike report)
        {
            var existingReport = await _repository.GetBikeReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this report.");
            }
            return await _repository.UpdateBikeReport(report);
        }

        public async Task<IEnumerable<Bike>> GetAllBikeReportsAsync()
        {
            return await _repository.GetAllBikeReportsAsync();
        }

        public async Task<Bike> GetBikeReportByIdAsync(int reportId)
        {
            return await _repository.GetBikeReportByIdAsync(reportId);
        }
    }

}
