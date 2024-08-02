using BringMeBackAPI.Models.Reports.Vehicles;
using BringMeBackAPI.Repository.Reports.Vehicle;

namespace BringMeBackAPI.Services.Reports.Vehicle.Service
{
    public class HeavyDutyMachineryReportService : IHeavyDutyMachineryReportService
    {
        private readonly IHeavyDutyMachineryReportRepository _repository;

        public HeavyDutyMachineryReportService(IHeavyDutyMachineryReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<HeavyDutyMachinery> CreateHeavyDutyMachineryReport(int userId, HeavyDutyMachinery report)
        {
            report.UserId = userId;
            return await _repository.CreateHeavyDutyMachineryReport(report);
        }

        public async Task<HeavyDutyMachinery> UpdateHeavyDutyMachineryReport(int userId, int reportId, HeavyDutyMachinery report)
        {
            var existingReport = await _repository.GetHeavyDutyMachineryReportByIdAsync(reportId);
            if (existingReport == null || existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this report.");
            }
            return await _repository.UpdateHeavyDutyMachineryReport(report);
        }

        public async Task<IEnumerable<HeavyDutyMachinery>> GetAllHeavyDutyMachineryReportsAsync()
        {
            return await _repository.GetAllHeavyDutyMachineryReportsAsync();
        }

        public async Task<HeavyDutyMachinery> GetHeavyDutyMachineryReportByIdAsync(int reportId)
        {
            return await _repository.GetHeavyDutyMachineryReportByIdAsync(reportId);
        }
    }

}
