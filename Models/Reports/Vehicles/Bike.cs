namespace BringMeBackAPI.Models.Reports.Vehicles
{
    public class Bike : Vehicle
    {
        // Additional car-specific properties
        public string BodyType { get; set; } // Body type (e.g., Sedan, Hatchback, SUV)
        public int WheelCount { get; set; } // Number of doors
        public bool IsConvertible { get; set; } // Whether the car is a convertible
        public string Drivetrain { get; set; } // Drivetrain type (e.g., FWD, RWD, AWD)
    }
}
