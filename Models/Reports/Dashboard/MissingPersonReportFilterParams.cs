namespace BringMeBackAPI.Models.Reports.Dashboard
{
    public class MissingPersonReportFilterParams
    {
        // Define properties based on what filters you need
        public string FullName { get; set; }
        public string LastSeenLocation { get; set; }

        public string Gender { get; set; }

        public float Height { get; set; }
        public float Weight { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string PossibleDestinations { get; set; }
        public string MedicalConditions { get; set; }


        public string Nationality { get; set; }
        public DateTime? LastSeenDateTimeFrom { get; set; }
        public DateTime? LastSeenDateTimeTo { get; set; }
        // Add more filters as required
    }
}
