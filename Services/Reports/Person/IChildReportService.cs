using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Services.Reports.Person
{
    public interface IChildReportService
    {
        Task<Child> CreateChildReport(int userId, Child report); // Only the user who creates it can update or delete
        Task<Child> UpdateChildReport(int userId, int reportId, Child report); // Only the owner can update
        Task<IEnumerable<Child>> GetAllChildReportsAsync();
        Task<Child> GetChildReportByIdAsync(int reportId);
    }

}
