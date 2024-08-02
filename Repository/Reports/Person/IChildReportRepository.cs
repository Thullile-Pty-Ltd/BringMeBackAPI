using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Repository.Reports.Person
{
    public interface IChildReportRepository
    {
        Task<Child> CreateChildReport(Child report);
        Task<Child> UpdateChildReport(Child report);
        Task<IEnumerable<Child>> GetAllChildReportsAsync();
        Task<Child> GetChildReportByIdAsync(int reportId);
    }

}
