using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Services.Reports.Person
{
    public interface ISeniorCitizenReportService
    {
        Task<SeniorCitizen> CreateSeniorCitizenReport(int userId, SeniorCitizen report);
        Task<SeniorCitizen> UpdateSeniorCitizenReport(int userId, int reportId, SeniorCitizen report);
        Task<IEnumerable<SeniorCitizen>> GetAllSeniorCitizenReportsAsync();
        Task<SeniorCitizen> GetSeniorCitizenReportByIdAsync(int reportId);
    }

}
