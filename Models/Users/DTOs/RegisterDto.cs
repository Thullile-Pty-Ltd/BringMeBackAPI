using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Users.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }

        // UserRole-specific properties
        public CommunityMemberDto? CommunityMember { get; set; }
        public DonorSupporterDto? DonorSupporter { get; set; }
        public FamilyMemberDto? FamilyMember { get; set; }
        public OrganizationUserDto? OrganizationUser { get; set; }
        public PublicAuthorityDto? PublicAuthority { get; set; }
        public VolunteerDto? Volunteer { get; set; }
    }

    public class CommunityMemberDto
    {
        public string CommunityRole { get; set; }
        public string CommunityAffiliation { get; set; }
        public bool Verification { get; set; }
    }

    public class DonorSupporterDto
    {
        public string DonationPreference { get; set; }
        public string MessageOfSupport { get; set; }
        public string PaymentInformation { get; set; }
    }

    public class FamilyMemberDto
    {
        public string RelationToMissingPerson { get; set; }
        public string DetailsOfMissingPerson { get; set; }
        public string UploadPhoto { get; set; }
    }

    public class OrganizationUserDto
    {
        public string OrganizationName { get; set; }
        public string OrganizationType { get; set; }
        public string RegistrationNumber { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
    }

    public class PublicAuthorityDto
    {
        public string PositionOrAgency { get; set; }
        public bool Authorization { get; set; }
        public string AccessCredentials { get; set; }
    }

    public class VolunteerDto
    {
        public string VolunteerExperience { get; set; }
        public string Availability { get; set; }
        public string InterestArea { get; set; }
    }

}
