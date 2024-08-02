using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Services.Reports.Item
{
    public interface IEquipmentService
    {
        Task<Equipment> CreateEquipmentReport(int userId, Equipment report);
        Task<Equipment> UpdateEquipmentReport(int userId, int reportId, Equipment report);
        Task<IEnumerable<Equipment>> GetAllEquipmentReportsAsync();
        Task<Equipment> GetEquipmentReportByIdAsync(int reportId);
    }

}
