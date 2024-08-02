using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Repository.Reports;
using BringMeBackAPI.Services.Users.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class MissingItemReportService : IMissingItemReportService
    {
        private readonly IMissingItemReportRepository _missingItemReportRepository;
        private readonly IUserService _userService;

        public MissingItemReportService(IMissingItemReportRepository missingItemReportRepository, IUserService userService)
        {
            _missingItemReportRepository = missingItemReportRepository;
            _userService = userService;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<MissingItemReport> CreateMissingItemReport(int userId, MissingItemReport report)
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
            return await _missingItemReportRepository.CreateMissingItemReport(report);
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
        public async Task<MissingItemReport> UpdateMissingItemReport(int userId, int id, MissingItemReport report)
        {
            var existingReport = await _missingItemReportRepository.GetMissingItemReportByIdAsync(id);
            if (existingReport == null)
            {
                throw new Exception("BaseReport not found.");
            }

            // Ensure the logged-in user is the owner of the report
            if (existingReport.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to update this report.");
            }

            // Update the existing properties
            existingReport.Description = report.Description;
            existingReport.ReportType = report.ReportType;
            existingReport.IsResolved = report.IsResolved;
            existingReport.IsArchived = report.IsArchived;

            // Update the new properties
            existingReport.ItemName = report.ItemName;
            existingReport.ItemDescription = report.ItemDescription;
            existingReport.SerialNumber = report.SerialNumber;
            existingReport.UniqueIdentifiers = report.UniqueIdentifiers;
            existingReport.LastKnownLocation = report.LastKnownLocation;
            existingReport.LastSeenDateTime = report.LastSeenDateTime;
            existingReport.CircumstancesOfLoss = report.CircumstancesOfLoss;
            existingReport.OwnerName = report.OwnerName;
            existingReport.OwnerPhoneNumber = report.OwnerPhoneNumber;
            existingReport.OwnerEmailAddress = report.OwnerEmailAddress;
            existingReport.RecentPhotos = report.RecentPhotos;
            existingReport.EstimatedValue = report.EstimatedValue;
            existingReport.RewardOffered = report.RewardOffered;
            existingReport.VideoUrl = report.VideoUrl;

            return await _missingItemReportRepository.UpdateMissingItemReport(existingReport);
        }


        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MissingItemReport>> GetAllMissingItemReportsAsync()
        {
            return await _missingItemReportRepository.GetAllMissingItemReportsAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<MissingItemReport> GetMissingItemReportByIdAsync(int reportId)
        {
            return await _missingItemReportRepository.GetMissingItemReportByIdAsync(reportId);
        }

        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MissingItemReport>> FilterMissingItemReportsAsync(MissingItemReportFilterParams filterParams)
        {
            return await _missingItemReportRepository.FilterMissingItemReportsAsync(filterParams);
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetMissingItemReportsStatisticsAsync()
        {
            return await _missingItemReportRepository.GetMissingItemReportsStatisticsAsync();
        }
    }
}
