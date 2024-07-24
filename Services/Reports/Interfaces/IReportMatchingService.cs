using BringMeBackAPI.Models.Reports;

namespace BringMeBackAPI.Services.Reports.Interfaces
{
    public interface IReportMatchingService
    {
        Task<(bool isMatch, MissingPersonReport matchedReport, double matchPercentage)> FindMissingPersonMatchingReportAsync(MissingPersonReport newReport);
        Task<(bool isMatch, FoundPersonReport matchedReport, double matchPercentage)> FindFoundPersonMatchingReportAsync(FoundPersonReport newReport);

        Task<(bool isMatch, MissingItemReport matchedReport, double matchPercentage)> FindMissingItemMatchingReportAsync(MissingItemReport newReport);

        Task<(bool isMatch, FoundItemReport matchedReport, double matchPercentage)> FindFoundItemMatchingReportAsync(FoundItemReport newReport);

    }

}
