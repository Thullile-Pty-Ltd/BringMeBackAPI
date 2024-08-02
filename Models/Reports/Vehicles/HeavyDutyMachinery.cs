namespace BringMeBackAPI.Models.Reports.Vehicles
{
    public class HeavyDutyMachinery : Vehicle
    {
        public string MachineType { get; set; } // E.g., Excavator, Bulldozer
        public string FuelType { get; set; } // Diesel, Gasoline, Electric
        public int UsageHours { get; set; } // Total hours of operation

        // Additional fields specific to heavy machinery
        public string Manufacturer { get; set; } // Manufacturer of the machinery
        public string SerialNumber { get; set; } // Serial number
        public string OperationalStatus { get; set; } // Current operational status (e.g., Operational, Under maintenance)
        public DateTime LastServiceDate { get; set; } // Date of the last service
        public List<string> ServiceRecords { get; set; } // Records of services and repairs
    }

}
