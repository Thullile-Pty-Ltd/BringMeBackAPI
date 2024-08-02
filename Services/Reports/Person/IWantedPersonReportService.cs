using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Services.Reports.Person
{
    public interface IWantedPersonReportService
    {
        Task<WantedPerson> CreateWantedPersonReport(int userId, WantedPerson report);
        Task<WantedPerson> UpdateWantedPersonReport(int userId, int reportId, WantedPerson report);
        Task<IEnumerable<WantedPerson>> GetAllWantedPersonReportsAsync();
        Task<WantedPerson> GetWantedPersonReportByIdAsync(int reportId);
    }

}
