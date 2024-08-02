using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Repository.Reports.Person
{
    public interface IWantedPersonReportRepository
    {
        Task<WantedPerson> CreateWantedPersonReport(WantedPerson report);
        Task<WantedPerson> UpdateWantedPersonReport(WantedPerson report);
        Task<IEnumerable<WantedPerson>> GetAllWantedPersonReportsAsync();
        Task<WantedPerson> GetWantedPersonReportByIdAsync(int reportId);
    }

}
