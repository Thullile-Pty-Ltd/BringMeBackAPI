using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Services.Reports.Item
{
    public interface IDeviceService
    {
        Task<Device> CreateDeviceReport(int userId, Device report);
        Task<Device> UpdateDeviceReport(int userId, int reportId, Device report);
        Task<IEnumerable<Device>> GetAllDeviceReportsAsync();
        Task<Device> GetDeviceReportByIdAsync(int reportId);
    }

}
