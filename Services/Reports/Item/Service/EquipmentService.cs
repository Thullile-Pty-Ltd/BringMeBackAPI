using BringMeBackAPI.Models.Reports.Items;
using BringMeBackAPI.Repository.Reports.Item;

namespace BringMeBackAPI.Services.Reports.Item.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRepository _repository;

        public EquipmentService(IEquipmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Equipment> CreateEquipmentReport(int userId, Equipment report)
        {
            report.UserId = userId;
            return await _repository.CreateEquipmentReport(report);
        }

        public async Task<Equipment> UpdateEquipmentReport(int userId, int reportId, Equipment report)
        {
            var existingReport = await _repository.GetEquipmentReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                return null; // Handle unauthorized access
            }

            report.ReportId = reportId;
            return await _repository.UpdateEquipmentReport(report);
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipmentReportsAsync()
        {
            return await _repository.GetAllEquipmentReportsAsync();
        }

        public async Task<Equipment> GetEquipmentReportByIdAsync(int reportId)
        {
            return await _repository.GetEquipmentReportByIdAsync(reportId);
        }
    }

}
