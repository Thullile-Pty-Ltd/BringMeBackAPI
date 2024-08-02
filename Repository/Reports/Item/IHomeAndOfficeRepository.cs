using BringMeBackAPI.Models.Reports.Items;

namespace BringMeBackAPI.Repository.Reports.Item
{
    public interface IHomeAndOfficeRepository
    {
        Task<HomeAndOffice> CreateHomeAndOfficeReport(HomeAndOffice report);
        Task<HomeAndOffice> UpdateHomeAndOfficeReport(HomeAndOffice report);
        Task<IEnumerable<HomeAndOffice>> GetAllHomeAndOfficeReportsAsync();
        Task<HomeAndOffice> GetHomeAndOfficeReportByIdAsync(int reportId);
    }

}
