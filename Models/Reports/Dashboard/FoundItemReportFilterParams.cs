namespace BringMeBackAPI.Models.Reports.Dashboard
{
    public class FoundItemReportFilterParams
    {
        // Define properties based on what filters you need
        public string? ItemName { get; set; }
        public string? FoundLocation { get; set; }
        public DateTime? FoundDateTimeFrom { get; set; }
        public DateTime? FoundDateTimeTo { get; set; }
        // Add more filters as required
    }
}
