namespace BringMeBackAPI.Models.Reports.Persons
{
    public class WantedPerson : Person
    {
        public decimal RewardAmount { get; set; }
        public string CrimeDetails { get; set; }

        // Additional fields related to wanted persons
        public string WantedBy { get; set; } // Law enforcement agency that issued the warrant
        public string WarrantNumber { get; set; } // Warrant number
        public DateTime WantedSince { get; set; } // Date since the person has been wanted
        public List<string> KnownAssociates { get; set; } // Names and details of known associates
        public string LastKnownAddress { get; set; } // Last known address of the wanted person
        public string Alias { get; set; } // Any known aliases
    }
}
