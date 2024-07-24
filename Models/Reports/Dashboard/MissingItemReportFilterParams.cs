namespace BringMeBackAPI.Models.Reports.Dashboard
{
    public class MissingItemReportFilterParams
    {
        // Define properties based on what filters you need
        public string? ItemName { get; set; }
        public string? LastKnownLocation { get; set; }
        public DateTime? LastSeenDateTimeFrom { get; set; }
        public DateTime? LastSeenDateTimeTo { get; set; }
        // Add more filters as required
    }
}
