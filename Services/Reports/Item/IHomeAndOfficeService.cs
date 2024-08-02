using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Services.Reports.Item
{
    public interface IHomeAndOfficeService
    {
        Task<HomeAndOffice> CreateHomeAndOfficeReport(int userId, HomeAndOffice report);
        Task<HomeAndOffice> UpdateHomeAndOfficeReport(int userId, int reportId, HomeAndOffice report);
        Task<IEnumerable<HomeAndOffice>> GetAllHomeAndOfficeReportsAsync();
        Task<HomeAndOffice> GetHomeAndOfficeReportByIdAsync(int reportId);
    }

}
