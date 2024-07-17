namespace BringMeBackAPI.Models.Reports.Dashboard
{
    public class FoundPersonReportFilterParams
    {
        // Define properties based on what filters you need
        public string FullName { get; set; }
        public string FoundLocation { get; set; }
        public DateTime? FoundDateTimeFrom { get; set; }
        public DateTime? FoundDateTimeTo { get; set; }
        // Add more filters as required
    }
}
