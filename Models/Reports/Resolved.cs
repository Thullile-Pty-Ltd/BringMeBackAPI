namespace BringMeBackAPI.Models.Reports
{
    public class Resolved 
    {
        public int CaseID { get; set; } // Unique identifier for the resolved case
        public DateTime ResolvedDate { get; set; } // Date when the case was resolved
        public string ResolutionDetails { get; set; } // Description of how the case was resolved
                                                      // Additional fields related to resolution can be added here.
    }
}
