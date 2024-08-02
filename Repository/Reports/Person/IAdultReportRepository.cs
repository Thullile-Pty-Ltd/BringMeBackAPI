using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Repository.Reports.Person
{
    public interface IAdultReportRepository
    {
        Task<Adult> CreateAdultReport(Adult report);
        Task<Adult> UpdateAdultReport(Adult report);
        Task<IEnumerable<Adult>> GetAllAdultReportsAsync();
        Task<Adult> GetAdultReportByIdAsync(int reportId);
    }

}
