using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Repository.Reports;
using BringMeBackAPI.Services.Users.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class FoundItemReportService : IFoundItemReportService
    {
        private readonly IFoundItemReportRepository _foundItemReportRepository;
        private readonly IUserService _userService;

        public FoundItemReportService(IFoundItemReportRepository foundItemReportRepository, IUserService userService)
        {
            _foundItemReportRepository = foundItemReportRepository;
            _userService = userService;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<FoundItemReport> CreateFoundItemReport(int userId, FoundItemReport report)
        {
            // Ensure the UserId in the report matches the logged-in user
            report.UserId = userId;

            // Fetch the user details from the database
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Populate the flattened user data in the report
            report.UserId = user.Id;
            report.UserName = user.Name;
            report.UserEmail = user.Email;
            report.PhoneNumber = user.PhoneNumber;
            report.Location = user.Location;
            report.Role = user.Role;

            // Set other report properties as needed
            report.CreatedAt = DateTime.UtcNow;
            return await _foundItemReportRepository.CreateFoundItemReport(report);
        }

        /// <summary>
        ///             EDIT / UPDATE
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<FoundItemReport> UpdateFoundItemReport(int userId, int id, FoundItemReport report)
        {
            var existingReport = await _foundItemReportRepository.GetFoundItemReportByIdAsync(id);
            if (existingReport == null)
            {
                throw new Exception("BaseReport not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this report.");
            }

            // Update the report details
            existingReport.Description = report.Description;
            existingReport.ReportType = report.ReportType;
            existingReport.IsResolved = report.IsResolved;
            existingReport.IsArchived = report.IsArchived;

            // Update the additional fields
            existingReport.ItemName = report.ItemName;
            existingReport.ItemDescription = report.ItemDescription;
            existingReport.SerialNumber = report.SerialNumber;
            existingReport.UniqueIdentifiers = report.UniqueIdentifiers;
            existingReport.FoundLocation = report.FoundLocation;
            existingReport.FoundDateTime = report.FoundDateTime;
            existingReport.ConditionOfItemWhenFound = report.ConditionOfItemWhenFound;
            existingReport.ReportingPersonName = report.ReportingPersonName;
            existingReport.ReportingPersonPhoneNumber = report.ReportingPersonPhoneNumber;
            existingReport.ReportingPersonEmailAddress = report.ReportingPersonEmailAddress;
            existingReport.RecentPhotos = report.RecentPhotos;
            existingReport.CircumstancesOfFinding = report.CircumstancesOfFinding;
            existingReport.VideoUrl = report.VideoUrl;

            return await _foundItemReportRepository.UpdateFoundItemReport(existingReport);
        }


        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FoundItemReport>> GetAllFoundItemReportsAsync()
        {
            return await _foundItemReportRepository.GetAllFoundItemReportsAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<FoundItemReport> GetFoundItemReportByIdAsync(int reportId)
        {
            return await _foundItemReportRepository.GetFoundItemReportByIdAsync(reportId);
        }


        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FoundItemReport>> FilterFoundItemReportsAsync(FoundItemReportFilterParams filterParams)
        {
            return await _foundItemReportRepository.FilterFoundItemReportsAsync(filterParams);
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetFoundItemReportsStatisticsAsync()
        {
            return await _foundItemReportRepository.GetFoundItemReportsStatisticsAsync();
        }
    }
}
