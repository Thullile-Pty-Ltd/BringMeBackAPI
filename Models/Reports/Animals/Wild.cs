namespace BringMeBackAPI.Models.Reports.Animals
{
    public class Wild : Animal
    {
        // Livestock-specific properties        
        public string HealthStatus { get; set; } // General health status
        public List<string> RecentMovements { get; set; } // Recent movements or transfers of the livestock
    }
}
