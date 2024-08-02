namespace BringMeBackAPI.Models.Reports.Items
{
    public class Item : BaseReport
    {
        // Item-specific properties
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal EstimatedValue { get; set; }
        public List<string> Photos { get; set; } // URLs or paths to photos of the item
        public string LastSeenLocation { get; set; }
        public DateTime LastSeenDateTime { get; set; }
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
