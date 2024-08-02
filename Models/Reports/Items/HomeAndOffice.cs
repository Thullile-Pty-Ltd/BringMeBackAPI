namespace BringMeBackAPI.Models.Reports.Items
{
    public class HomeAndOffice : Item
    {
        // Home And Office-specific properties
        public string Room { get; set; } // Room where the item was last seen
        public bool IsInsured { get; set; } // Whether the item is insured
        public string InsuranceDetails { get; set; } // Details of the insurance policy
        public string Manufacturer { get; set; } // Manufacturer of the item
        public string WarrantyDetails { get; set; } // Warranty information
        public DateTime WarrantyExpiry { get; set; } // Warranty expiry date
        public List<string> Accessories { get; set; } // List of accessories associated with the item
    }

}
