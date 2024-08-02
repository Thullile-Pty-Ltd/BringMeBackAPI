namespace BringMeBackAPI.Models.Reports.Persons
{
    public class SeniorCitizen : Person
    {
        public bool HasPets { get; set; }

        // Additional senior citizen-specific fields
        public string HealthStatus { get; set; } // General health condition
        public string MobilityStatus { get; set; } // Independent, uses a cane, wheelchair-bound, etc.
        public List<string> Medications { get; set; } // List of medications being taken
        public string EmergencyContact { get; set; } // Emergency contact person and their details
        public List<string> Caregivers { get; set; } // Names and contact details of caregivers

    }
}
