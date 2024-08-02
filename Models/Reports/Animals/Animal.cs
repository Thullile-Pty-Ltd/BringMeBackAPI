namespace BringMeBackAPI.Models.Reports.Animals
{
    public class Animal : BaseReport
    {
        // Animal-specific properties
        public string Name { get; set; } // Name of the animal
        public string Species { get; set; } // Species of the animal
        public string Breed { get; set; } // Breed of the animal
        public string Gender { get; set; } // Gender of the animal
        public int Age { get; set; } // Age of the animal
        public string Color { get; set; } // Color of the animal
        public string PhysicalDescription { get; set; } // Physical description (size, markings, etc.)
        public List<string> Photos { get; set; } // URLs or paths to photos of the animal
        public string LastSeenLocation { get; set; } // Last known location
        public DateTime LastSeenDateTime { get; set; } // Date and time when last seen
        public string MedicalConditions { get; set; } // Any known medical conditions
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
