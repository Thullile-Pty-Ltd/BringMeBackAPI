namespace BringMeBackAPI.Models.Reports.Items
{
    public class Device : Item
    {
        // Device-specific properties
        public string DeviceType { get; set; } // Type of device (e.g., Laptop, Smartphone, Tablet)
        public string OperatingSystem { get; set; } // Operating system (e.g., Windows, iOS, Android)
        public int StorageCapacity { get; set; } // Storage capacity in GB
        public int RAM { get; set; } // RAM size in GB
        public string Processor { get; set; } // Processor details
        public string IMEI { get; set; } // IMEI number for mobile devices
        public bool IsLocked { get; set; } // Whether the device is locked
        public List<string> InstalledSoftware { get; set; } // List of installed software or apps
        public List<string> Accessories { get; set; } // List of accessories associated with the device
        public DateTime LastBackupDate { get; set; } // Date of the last backup
    }

}
