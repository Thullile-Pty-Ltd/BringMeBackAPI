namespace BringMeBackAPI.Models.Reports.Animals
{
    public class Pet : Animal
    {
        // Pet-specific properties
        public string MicrochipNumber { get; set; } // Microchip number for identification
        public string OwnerName { get; set; } // Name of the owner
        public string OwnerContact { get; set; } // Contact details of the owner
        public string FavoriteFood { get; set; } // Favorite food of the pet
        public string FavoriteActivity { get; set; } // Favorite activity or toy
        public bool IsNeutered { get; set; } // Whether the pet is neutered/spayed
        public string BehavioralTraits { get; set; } // Notable behavioral traits
        public List<string> KnownCommands { get; set; } // Commands known by the pet
        public string VeterinarianName { get; set; } // Name of the veterinarian
        public string VeterinarianContact { get; set; } // Contact details of the veterinarian
    }

}
