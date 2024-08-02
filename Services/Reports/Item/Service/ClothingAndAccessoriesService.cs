using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;

namespace BringMeBackAPI.Services.Reports.Item.Service
{
    public class ClothingAndAccessoriesService : IClothingAndAccessoriesService
    {
        private readonly IClothingAndAccessoriesRepository _repository;

        public ClothingAndAccessoriesService(IClothingAndAccessoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClothingAndAccessories> CreateClothingAndAccessoriesReport(int userId, ClothingAndAccessories report)
        {
            report.UserId = userId;
            return await _repository.CreateClothingAndAccessoriesReport(report);
        }

        public async Task<ClothingAndAccessories> UpdateClothingAndAccessoriesReport(int userId, int reportId, ClothingAndAccessories report)
        {
            var existingReport = await _repository.GetClothingAndAccessoriesReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle unauthorized access
            }

            report.ReportId = reportId;
            return await _repository.UpdateClothingAndAccessoriesReport(report);
        }

        public async Task<IEnumerable<ClothingAndAccessories>> GetAllClothingAndAccessoriesReportsAsync()
        {
            return await _repository.GetAllClothingAndAccessoriesReportsAsync();
        }

        public async Task<ClothingAndAccessories> GetClothingAndAccessoriesReportByIdAsync(int reportId)
        {
            return await _repository.GetClothingAndAccessoriesReportByIdAsync(reportId);
        }
    }

}
