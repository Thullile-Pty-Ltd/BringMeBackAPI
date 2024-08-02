using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Repository.Reports.Item
{
    public interface IDeviceRepository
    {
        Task<Device> CreateDeviceReport(Device report);
        Task<Device> UpdateDeviceReport(Device report);
        Task<IEnumerable<Device>> GetAllDeviceReportsAsync();
        Task<Device> GetDeviceReportByIdAsync(int reportId);
    }

}
