namespace BringMeBackAPI.Models.Reports.Animals
{
    public class Livestock : Animal
    {
        // Livestock-specific properties
        public string EarTagNumber { get; set; } // Ear tag number for identification
        public string HerdName { get; set; } // Name of the herd
        public string FarmName { get; set; } // Name of the farm where the livestock belongs
        public string FarmAddress { get; set; } // Address of the farm
        public bool IsVaccinated { get; set; } // Whether the livestock is vaccinated
        public string VaccinationDetails { get; set; } // Details of vaccinations
        public string HealthStatus { get; set; } // General health status
        public List<string> RecentMovements { get; set; } // Recent movements or transfers of the livestock
    }

}
