using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users
{
    public class FamilyMember : User
    {
        [MaxLength(100, ErrorMessage = "Relation to missing person cannot exceed 100 characters.")]
        public string RelationToMissingPerson { get; set; }

        public string DetailsOfMissingPerson { get; set; }

        [MaxLength(200, ErrorMessage = "File path or URL cannot exceed 200 characters.")]
        public string UploadPhoto { get; set; }
    }

}
