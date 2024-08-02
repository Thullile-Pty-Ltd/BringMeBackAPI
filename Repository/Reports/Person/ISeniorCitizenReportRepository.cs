using BringMeBackAPI.Models.Reports.Persons;

namespace BringMeBackAPI.Repository.Reports.Person
{
    public interface ISeniorCitizenReportRepository
    {
        Task<SeniorCitizen> CreateSeniorCitizenReport(SeniorCitizen report);
        Task<SeniorCitizen> UpdateSeniorCitizenReport(SeniorCitizen report);
        Task<IEnumerable<SeniorCitizen>> GetAllSeniorCitizenReportsAsync();
        Task<SeniorCitizen> GetSeniorCitizenReportByIdAsync(int reportId);
    }

}
