namespace BringMeBackAPI.Models.Reports.Items
{
    public class Equipment : Item
    {
        // Equipment-specific properties
        public string EquipmentType { get; set; } // Type of equipment (e.g., Power tool, Construction equipment)
        public string Brand { get; set; } // Brand of the equipment
        public string Model { get; set; } // Model of the equipment
        public string SerialNumber { get; set; } // Serial number of the equipment
        public DateTime PurchaseDate { get; set; } // Date of purchase
        public decimal EstimatedValue { get; set; } // Estimated value of the equipment
        public string Condition { get; set; } // Condition of the equipment (e.g., New, Used)
        public List<string> Photos { get; set; } // URLs or paths to photos of the equipment
        public string LastSeenLocation { get; set; } // Last known location
        public DateTime LastSeenDateTime { get; set; } // Date and time when last seen
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
