namespace BringMeBackAPI.Models.Reports.Vehicles
{
    public class Truck : Vehicle
    {
        // Additional truck-specific properties
        public string CargoCapacity { get; set; } // Cargo capacity of the truck
        public int AxleCount { get; set; } // Number of axles
        public string TruckType { get; set; } // Type of truck (e.g., Pickup, Semi-trailer)
        public string TrailerType { get; set; } // Type of trailer if applicable
    }

}
