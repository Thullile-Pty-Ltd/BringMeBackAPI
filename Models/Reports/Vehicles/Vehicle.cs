namespace BringMeBackAPI.Models.Reports.Vehicles
{
    public class Vehicle : BaseReport
    {
        public string Make { get; set; }
        public string Model { get; set; }

        // Additional vehicle-specific properties
        public string VIN { get; set; } // Vehicle Identification Number
        public string LicensePlate { get; set; } // License plate number
        public int Year { get; set; } // Year of manufacture
        public string Color { get; set; } // Color of the vehicle
        public string EngineSize { get; set; } // Engine size
        public string FuelType { get; set; } // Fuel type (e.g., Gasoline, Diesel, Electric)
        public int Mileage { get; set; } // Mileage
        public string RegistrationState { get; set; } // State of registration
        public List<string> Photos { get; set; } // URLs or paths to photos of the vehicle
        public string LastSeenLocation { get; set; } // Last known location
        public DateTime LastSeenDateTime { get; set; } // Date and time when last seen
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
