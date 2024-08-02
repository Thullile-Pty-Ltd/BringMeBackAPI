using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Repository.Reports.Interfaces;
using BringMeBackAPI.Services.Reports.Interfaces;

namespace BringMeBackAPI.Services.Reports.Services
{
    public class ReportMatchingService : IReportMatchingService
    {
        private readonly IMissingPersonReportRepository _missingPersonReportRepository;
        private readonly IFoundPersonReportRepository _foundPersonReportRepository;
        private readonly IMissingItemReportRepository _missingItemReportRepository;
        private readonly IFoundItemReportRepository _foundItemReportRepository;

        public ReportMatchingService(IMissingPersonReportRepository missingPersonReportRepository, 
            IFoundPersonReportRepository foundPersonReportRepository, 
            IMissingItemReportRepository missingItemReportRepository, IFoundItemReportRepository foundItemReportRepository)
        {
            _missingPersonReportRepository = missingPersonReportRepository;
            _foundPersonReportRepository = foundPersonReportRepository;
            _foundItemReportRepository = foundItemReportRepository;
            _missingItemReportRepository = missingItemReportRepository;
        }

        /// <summary>
        ///             MATCH MISSING PERSON
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        public async Task<(bool isMatch, MissingPersonReport matchedReport, double matchPercentage)> FindMissingPersonMatchingReportAsync(MissingPersonReport newReport)
        {
            var allMissingReports = await _missingPersonReportRepository.GetAllMissingPersonReportsAsync();
            var allFoundReports = await _foundPersonReportRepository.GetAllFoundPersonReportsAsync();

            var allReports = allMissingReports.Cast<BaseReport>().Concat(allFoundReports.Cast<BaseReport>());

            MissingPersonReport? bestMatch = null;
            double highestMatchPercentage = 0;

            foreach (var report in allReports)
            {
                double matchPercentage = 0;

                if (report is MissingPersonReport missingReport)
                {
                    matchPercentage = CalculateMissingPersonMatchPercentage(newReport, missingReport);
                }
                else if (report is FoundPersonReport foundReport)
                {
                    matchPercentage = CalculateMissingPersonMatchPercentage(newReport, foundReport);
                }

                if (matchPercentage > highestMatchPercentage)
                {
                    highestMatchPercentage = matchPercentage;
                    bestMatch = report as MissingPersonReport;
                }
            }

            return (highestMatchPercentage >= 50, bestMatch, highestMatchPercentage); // Assuming 80% is the threshold
        }

        /// <summary>
        ///             MISSING PERSON REPORT MATCH PERCENTAGE
        /// </summary>
        /// <param name="newReport"></param>
        /// <param name="existingReport"></param>
        /// <returns></returns>
        private double CalculateMissingPersonMatchPercentage(MissingPersonReport newReport, BaseReport existingReport)
        {
            // Implement your matching logic here. For simplicity, we assume a basic comparison
            double score = 0;

            if (existingReport is MissingPersonReport missingReport)
            {
                if (newReport.FullName == missingReport.FullName) score += 20;
                if (newReport.Gender == missingReport.Gender) score += 10;
                if (newReport.DateOfBirth == missingReport.DateOfBirth) score += 20;
                if (newReport.Nationality == missingReport.Nationality) score += 10;

                if (newReport.Height == missingReport.Height) score += 5;
                if (newReport.Weight == missingReport.Weight) score += 5;
                if (newReport.EyeColor == missingReport.EyeColor) score += 5;
                if (newReport.HairColor == missingReport.HairColor) score += 5;
            }
            else if (existingReport is FoundPersonReport foundReport)
            {
                if (newReport.FullName == foundReport.FullName) score += 20;
                if (newReport.Gender == foundReport.Gender) score += 10;
                
                if (newReport.Nationality == foundReport.Nationality) score += 10;

                if (newReport.Height == foundReport.Height) score += 5;
                if (newReport.Weight == foundReport.Weight) score += 5;
                if (newReport.EyeColor == foundReport.EyeColor) score += 15;
                if (newReport.HairColor == foundReport.HairColor) score += 15;
            }

            return score; // A score out of 80
        }


        /// <summary>
        ///             MATCH FOUND PERSON
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        public async Task<(bool isMatch, FoundPersonReport matchedReport, double matchPercentage)> FindFoundPersonMatchingReportAsync(FoundPersonReport newReport)
        {
            var allMissingReports = await _missingPersonReportRepository.GetAllMissingPersonReportsAsync();
            var allFoundReports = await _foundPersonReportRepository.GetAllFoundPersonReportsAsync();

            var allReports = allFoundReports.Cast<BaseReport>().Concat(allMissingReports.Cast<BaseReport>());

            FoundPersonReport? bestMatch = null;
            double highestMatchPercentage = 0;

            foreach (var report in allReports)
            {
                double matchPercentage = 0;

                if (report is MissingPersonReport missingReport)
                {
                    matchPercentage = CalculateFoundPersonMatchPercentage(newReport, missingReport);
                }
                else if (report is FoundPersonReport foundReport)
                {
                    matchPercentage = CalculateFoundPersonMatchPercentage(newReport, foundReport);
                }

                if (matchPercentage > highestMatchPercentage)
                {
                    highestMatchPercentage = matchPercentage;
                    bestMatch = report as FoundPersonReport;
                }
            }

            return (highestMatchPercentage > 50, bestMatch, highestMatchPercentage); // Assuming 80% is the threshold
        }

        /// <summary>
        ///             FOUND PERSON REPORT MATCH PERCENTAGE
        /// </summary>
        /// <param name="newReport"></param>
        /// <param name="existingReport"></param>
        /// <returns></returns>
        private double CalculateFoundPersonMatchPercentage(FoundPersonReport newReport, BaseReport existingReport)
        {
            // Implement your matching logic here. For simplicity, we assume a basic comparison
            double score = 0;

            if (existingReport is MissingPersonReport missingReport)
            {
                if (newReport.FullName == missingReport.FullName) score += 20;
                if (newReport.Gender == missingReport.Gender) score += 10;
                
                if (newReport.Nationality == missingReport.Nationality) score += 10;

                if (newReport.Height == missingReport.Height) score += 5;
                if (newReport.Weight == missingReport.Weight) score += 15;
                if (newReport.EyeColor == missingReport.EyeColor) score += 5;
                if (newReport.HairColor == missingReport.HairColor) score += 15;
            }
            else if (existingReport is FoundPersonReport foundReport)
            {
                if (newReport.FullName == foundReport.FullName) score += 20;
                if (newReport.Gender == foundReport.Gender) score += 10;

                if (newReport.Nationality == foundReport.Nationality) score += 10;

                if (newReport.Height == foundReport.Height) score += 5;
                if (newReport.Weight == foundReport.Weight) score += 5;
                if (newReport.EyeColor == foundReport.EyeColor) score += 15;
                if (newReport.HairColor == foundReport.HairColor) score += 15;
            }

            return score; // A score out of 80
        }

        /// <summary>
        ///             MATCH MISSING ITEM
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        public async Task<(bool isMatch, MissingItemReport matchedReport, double matchPercentage)> FindMissingItemMatchingReportAsync(MissingItemReport newReport)
        {
            var allMissingReports = await _missingItemReportRepository.GetAllMissingItemReportsAsync();
            var allFoundReports = await _foundItemReportRepository.GetAllFoundItemReportsAsync();

            var allReports = allFoundReports.Cast<BaseReport>().Concat(allMissingReports.Cast<BaseReport>());

            MissingItemReport? bestMatch = null;
            double highestMatchPercentage = 0;

            foreach (var report in allReports)
            {
                double matchPercentage = 0;

                if (report is MissingItemReport missingReport)
                {
                    matchPercentage = CalculateItemMatchPercentage(newReport, missingReport);
                }
                else if (report is FoundItemReport foundReport)
                {
                    matchPercentage = CalculateItemMatchPercentage(newReport, foundReport);
                }

                if (matchPercentage > highestMatchPercentage)
                {
                    highestMatchPercentage = matchPercentage;
                    bestMatch = report as MissingItemReport;
                }
            }

            return (highestMatchPercentage > 50, bestMatch, highestMatchPercentage); // Assuming 50% is the threshold
        }

        /// <summary>
        ///             ITEM REPORT MATCH PERCENTAGE
        /// </summary>
        /// <param name="newReport"></param>
        /// <param name="existingReport"></param>
        /// <returns></returns>
        private double CalculateItemMatchPercentage(MissingItemReport newReport, BaseReport existingReport)
        {
            double score = 0;
            int totalPossibleScore = 0;

            if (existingReport is MissingItemReport missingReport)
            {
                totalPossibleScore += 30; // Adjust based on importance of properties

                if (newReport.ItemName == missingReport.ItemName) score += 15;
                if (newReport.SerialNumber == missingReport.SerialNumber) score += 10;
                if (newReport.UniqueIdentifiers == missingReport.UniqueIdentifiers) score += 5;
                if (newReport.LastKnownLocation == missingReport.LastKnownLocation) score += 10;
                if (newReport.LastSeenDateTime.Date == missingReport.LastSeenDateTime.Date) score += 10;
                if (newReport.EstimatedValue == missingReport.EstimatedValue) score += 10;
                if (newReport.RewardOffered == missingReport.RewardOffered) score += 10;
                if (newReport.VideoUrl == missingReport.VideoUrl) score += 5;
            }
            else if (existingReport is FoundItemReport foundReport)
            {
                totalPossibleScore += 30;

                if (newReport.ItemName == foundReport.ItemName) score += 15;
                if (newReport.SerialNumber == foundReport.SerialNumber) score += 10;
                if (newReport.UniqueIdentifiers == foundReport.UniqueIdentifiers) score += 5;
                if (newReport.LastKnownLocation == foundReport.FoundLocation) score += 10;
                if (newReport.LastSeenDateTime.Date == foundReport.FoundDateTime.Date) score += 10;
                if (newReport.VideoUrl == foundReport.VideoUrl) score += 5;
            }

            return totalPossibleScore > 0 ? (score / totalPossibleScore) * 100 : 0;
        }

        /// <summary>
        ///             MATCH FOUND ITEM
        /// </summary>
        /// <param name="newReport"></param>
        /// <returns></returns>
        public async Task<(bool isMatch, FoundItemReport matchedReport, double matchPercentage)> FindFoundItemMatchingReportAsync(FoundItemReport newReport)
        {
            var allMissingReports = await _missingItemReportRepository.GetAllMissingItemReportsAsync();
            var allFoundReports = await _foundItemReportRepository.GetAllFoundItemReportsAsync();

            var allReports = allFoundReports.Cast<BaseReport>().Concat(allMissingReports.Cast<BaseReport>());

            FoundItemReport? bestMatch = null;
            double highestMatchPercentage = 0;

            foreach (var report in allReports)
            {
                double matchPercentage = 0;

                if (report is MissingItemReport missingReport)
                {
                    matchPercentage = CalculateFoundItemMatchPercentage(newReport, missingReport);
                }
                else if (report is FoundItemReport foundReport)
                {
                    matchPercentage = CalculateFoundItemMatchPercentage(newReport, foundReport);
                }

                if (matchPercentage > highestMatchPercentage)
                {
                    highestMatchPercentage = matchPercentage;
                    bestMatch = report as FoundItemReport;
                }
            }

            return (highestMatchPercentage > 50, bestMatch, highestMatchPercentage); // Assuming 50% is the threshold
        }

        /// <summary>
        ///             FOUND ITEM REPORT MATCH PERCENTAGE
        /// </summary>
        /// <param name="newReport"></param>
        /// <param name="existingReport"></param>
        /// <returns></returns>
        private double CalculateFoundItemMatchPercentage(FoundItemReport newReport, BaseReport existingReport)
        {
            double score = 0;
            int totalPossibleScore = 0;

            if (existingReport is MissingItemReport missingReport)
            {
                totalPossibleScore += 30;

                if (newReport.ItemName == missingReport.ItemName) score += 15;
                if (newReport.SerialNumber == missingReport.SerialNumber) score += 10;
                if (newReport.UniqueIdentifiers == missingReport.UniqueIdentifiers) score += 5;
                if (newReport.ConditionOfItemWhenFound == missingReport.CircumstancesOfLoss) score += 10;
                if (newReport.FoundDateTime.Date == missingReport.LastSeenDateTime.Date) score += 10;
                if (newReport.VideoUrl == missingReport.VideoUrl) score += 5;
            }
            else if (existingReport is FoundItemReport foundReport)
            {
                totalPossibleScore += 30;

                if (newReport.ItemName == foundReport.ItemName) score += 15;
                if (newReport.SerialNumber == foundReport.SerialNumber) score += 10;
                if (newReport.UniqueIdentifiers == foundReport.UniqueIdentifiers) score += 5;
                if (newReport.FoundLocation == foundReport.FoundLocation) score += 10;
                if (newReport.FoundDateTime.Date == foundReport.FoundDateTime.Date) score += 10;
                if (newReport.ConditionOfItemWhenFound == foundReport.ConditionOfItemWhenFound) score += 5;
                if (newReport.VideoUrl == foundReport.VideoUrl) score += 5;
            }

            return totalPossibleScore > 0 ? (score / totalPossibleScore) * 100 : 0;
        }


    }

}
