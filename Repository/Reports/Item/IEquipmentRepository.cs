using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Repository.Reports.Item
{
    public interface IEquipmentRepository
    {
        Task<Equipment> CreateEquipmentReport(Equipment report);
        Task<Equipment> UpdateEquipmentReport(Equipment report);
        Task<IEnumerable<Equipment>> GetAllEquipmentReportsAsync();
        Task<Equipment> GetEquipmentReportByIdAsync(int reportId);
    }

}
