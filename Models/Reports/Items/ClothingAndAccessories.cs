namespace BringMeBackAPI.Models.Reports.Items
{
    public class ClothingAndAccessories : BaseReport
    {
        // Clothing and Accessories-specific properties
        public string Brand { get; set; } // Brand of the item
        public string Size { get; set; } // Size of the item
        public string Color { get; set; } // Color of the item
        public string Material { get; set; } // Material of the item
        public string Style { get; set; } // Style of the item (e.g., Casual, Formal)
        public string Description { get; set; } // Description of the item
        public List<string> Photos { get; set; } // URLs or paths to photos of the item
        public string LastSeenLocation { get; set; } // Last known location
        public DateTime LastSeenDateTime { get; set; } // Date and time when last seen
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
