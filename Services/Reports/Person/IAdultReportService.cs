using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Services.Reports.Person
{
    public interface IAdultReportService
    {
        Task<Adult> CreateAdultReport(int userId, Adult report);
        Task<Adult> UpdateAdultReport(int userId, int reportId, Adult report);
        Task<IEnumerable<Adult>> GetAllAdultReportsAsync();
        Task<Adult> GetAdultReportByIdAsync(int reportId);
    }

}
