namespace BringMeBackAPI.Models.Reports.Persons
{
    public class Adult : Person
    {
        public string Occupation { get; set; }

        // Additional adult-specific fields
        public string Workplace { get; set; }
        public string WorkplaceAddress { get; set; }
        public string MaritalStatus { get; set; } // Single, Married, Divorced, etc.
        public List<string> Hobbies { get; set; } // Hobbies or interests of the adult
    }
}
