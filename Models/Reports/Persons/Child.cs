namespace BringMeBackAPI.Models.Reports.Persons
{
    public class Child : Person
    {
        public string SchoolName { get; set; }

        // Additional child-specific fields
        public string Grade { get; set; }
        public string SchoolAddress { get; set; }
        public string GuardianName { get; set; }
        public string GuardianContact { get; set; }
        public List<string> FavoriteActivities { get; set; } // Favorite activities or hobbies of the child
    }

}
