using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Services.Users.Interfaces;
using BringMeBackAPI.Repository.Reports;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class FoundPersonReportService : IFoundPersonReportService
    {
        private readonly IFoundPersonReportRepository _foundPersonReportRepository;
        private readonly IUserService _userService;

        public FoundPersonReportService(IFoundPersonReportRepository foundPersonReportRepository, IUserService userService)
        {
            _foundPersonReportRepository = foundPersonReportRepository;
            _userService = userService;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<FoundPersonReport> CreateFoundPersonReport(int userId, FoundPersonReport report)
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
            return await _foundPersonReportRepository.CreateFoundPersonReport(report);
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
        public async Task<FoundPersonReport> UpdateFoundPersonReport(int userId, int id, FoundPersonReport report)
        {
            var existingReport = await _foundPersonReportRepository.GetFoundPersonReportByIdAsync(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
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

            // Update the new properties
            existingReport.FullName = report.FullName;
            existingReport.Nickname = report.Nickname;
            existingReport.Gender = report.Gender;
            existingReport.EstimatedAge = report.EstimatedAge;
            existingReport.Nationality = report.Nationality;
            existingReport.Height = report.Height;
            existingReport.Weight = report.Weight;
            existingReport.EyeColor = report.EyeColor;
            existingReport.HairColor = report.HairColor;
            existingReport.DistinguishingMarksOrFeatures = report.DistinguishingMarksOrFeatures;
            existingReport.FoundLocation = report.FoundLocation;
            existingReport.FoundDateTime = report.FoundDateTime;
            existingReport.ClothingAtTimeOfFinding = report.ClothingAtTimeOfFinding;
            existingReport.ConditionWhenFound = report.ConditionWhenFound;
            existingReport.ObservedMedicalConditions = report.ObservedMedicalConditions;
            existingReport.ObservedMedications = report.ObservedMedications;
            existingReport.ObservedMentalHealthStatus = report.ObservedMentalHealthStatus;
            existingReport.RecentPhotos = report.RecentPhotos;
            existingReport.VideoUrl = report.VideoUrl;

            return await _foundPersonReportRepository.UpdateFoundPersonReport(existingReport);
        }

        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FoundPersonReport>> GetAllFoundPersonReportsAsync()
        {
            return await _foundPersonReportRepository.GetAllFoundPersonReportsAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<FoundPersonReport> GetFoundPersonReportByIdAsync(int reportId)
        {
            return await _foundPersonReportRepository.GetFoundPersonReportByIdAsync(reportId);
        }

        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FoundPersonReport>> FilterFoundPersonReportsAsync(FoundPersonReportFilterParams filterParams)
        {
            return await _foundPersonReportRepository.FilterFoundPersonReportsAsync(filterParams);
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetFoundPersonReportsStatisticsAsync()
        {
            return await _foundPersonReportRepository.GetFoundPersonReportsStatisticsAsync();
        }
                
    }
}
