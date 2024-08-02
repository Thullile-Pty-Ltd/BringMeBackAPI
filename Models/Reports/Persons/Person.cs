namespace BringMeBackAPI.Models.Reports.Persons
{
    public class Person : BaseReport
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // Additional person-specific fields
        public string Gender { get; set; }
        public string NationalID { get; set; } // National Identification number
        public DateTime DateOfBirth { get; set; }
        public string PhysicalDescription { get; set; } // Height, weight, general physical description
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string TeethDescription { get; set; } // Description of the teeth
        public List<string> Photos { get; set; } // URLs or paths to photos of the person
        public string LastSeenLocation { get; set; }
        public DateTime LastSeenDateTime { get; set; }
        public string LastSeenClothing { get; set; } // Description of the clothing worn when last seen
        public string MedicalConditions { get; set; } // Any known medical conditions
        public List<string> ContactPersons { get; set; } // Names and contact details of persons to contact
    }

}
