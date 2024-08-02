namespace BringMeBackAPI.Models.Reports.Vehicles
{
    public class Bus : Vehicle
    {
        // Additional bus-specific properties
        public int SeatingCapacity { get; set; } // Seating capacity
        public string BusType { get; set; } // Type of bus (e.g., City bus, Coach)
        public string RouteNumber { get; set; } // Route number if applicable
        public bool IsAccessible { get; set; } // Whether the bus is wheelchair accessible
    }

}
