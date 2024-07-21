using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Reports.Dashboard;
using BringMeBackAPI.Repository.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Dashboards.Interfaces;
using BringMeBackAPI.Services.Users.Interfaces;

namespace BringMeBackAPI.Services.Reports.Dashboards
{
    public class MissingPersonReportService : IMissingPersonReportService
    {
        private readonly IMissingPersonReportRepository _missingPersonReportRepository;
        private readonly IUserService _userService;

        public MissingPersonReportService(IMissingPersonReportRepository missngPersonReportRepository, IUserService userService)
        {
            _missingPersonReportRepository = missngPersonReportRepository;
            _userService = userService;
        }

        /// <summary>
        ///             CREATE
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<MissingPersonReport> CreateMissingPersonReport(int userId, MissingPersonReport report)
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
            return await _missingPersonReportRepository.CreateMissingPersonReport(report);
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
        public async Task<MissingPersonReport> UpdateMissingPersonReport(int userId, int id, MissingPersonReport report)
        {
            var existingReport = await _missingPersonReportRepository.GetMissingPersonReportByIdAsync(id);
            if (existingReport == null)
            {
                throw new Exception("Report not found.");
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
            existingReport.FullName = report.FullName;
            existingReport.Nickname = report.Nickname;
            existingReport.Gender = report.Gender;
            existingReport.DateOfBirth = report.DateOfBirth;
            existingReport.IDNumber = report.IDNumber;
            existingReport.Nationality = report.Nationality;
            existingReport.Height = report.Height;
            existingReport.Weight = report.Weight;
            existingReport.EyeColor = report.EyeColor;
            existingReport.HairColor = report.HairColor;
            existingReport.DistinguishingMarksOrFeatures = report.DistinguishingMarksOrFeatures;
            existingReport.LastSeenLocation = report.LastSeenLocation;
            existingReport.LastSeenDateTime = report.LastSeenDateTime;
            existingReport.ClothingLastSeenWearing = report.ClothingLastSeenWearing;
            existingReport.PossibleDestinations = report.PossibleDestinations;
            existingReport.MedicalConditions = report.MedicalConditions;
            existingReport.MedicationsRequired = report.MedicationsRequired;
            existingReport.MentalHealthStatus = report.MentalHealthStatus;
            existingReport.PrimaryContactPerson = report.PrimaryContactPerson;
            existingReport.ContactPhoneNumber = report.ContactPhoneNumber;
            existingReport.ContactEmailAddress = report.ContactEmailAddress;
            existingReport.SocialMediaAccounts = report.SocialMediaAccounts;
            existingReport.RecentPhotos = report.RecentPhotos;
            existingReport.BriefDescriptionOfCircumstances = report.BriefDescriptionOfCircumstances;
            existingReport.VideoUrl = report.VideoUrl;

            return await _missingPersonReportRepository.UpdateMissingPersonReport(existingReport);
        }

        /// <summary>
        ///             GET ALL
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MissingPersonReport>> GetAllMissingPersonReportsAsync()
        {
            return await _missingPersonReportRepository.GetAllMissingPersonReportsAsync();
        }

        /// <summary>
        ///             GET BY ID
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public async Task<MissingPersonReport> GetMissingPersonReportByIdAsync(int reportId)
        {
            return await _missingPersonReportRepository.GetMissingPersonReportByIdAsync(reportId);
        }

        /// <summary>
        ///             FILTERS
        /// </summary>
        /// <param name="MissingPersonFilterParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MissingPersonReport>> FilterMissingPersonReportsAsync(MissingPersonReportFilterParams MissingPersonFilterParams)
        {
            return await _missingPersonReportRepository.FilterMissingPersonReportsAsync(MissingPersonFilterParams);
        }

        /// <summary>
        ///             STATISTICS
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetMissingPersonReportsStatisticsAsync()
        {
            // Implement logic to calculate and return statistics
            return await _missingPersonReportRepository.GetMissingPersonReportsStatisticsAsync();
        }
    }
}
