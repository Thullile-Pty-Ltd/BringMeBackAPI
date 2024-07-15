using BringMeBackAPI.Models.Users;

namespace BringMeBackAPI.Services.Users.Interfaces
{
    public interface IUserservice
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role);

        Task<User> CreateUserAsync(User User);
        Task<User> UpdateUserAsync(int id, User User);
        Task<bool> DeleteUserAsync(int id);
    }

    public interface IOrganizationUserService
    {
        Task<OrganizationUser> CreateOrganizationUserAsync(User User);
    }

    public interface ICommunityMemberService
    {
        Task<CommunityMember> CreateCommunityMemberAsync(User User);
    }

    public interface IFamilyMemberService
    {
        Task<FamilyMember> CreateFamilyMemberAsync(User User);
    }

    public interface IPublicAuthorityService
    {
        Task<PublicAuthority> CreatePublicAuthorityAsync(User User);
    }

    public interface IVolunteerService
    {
        Task<Volunteer> CreateVolunteerAsync(User User);
    }

    public interface IDonorSupporterService
    {
        Task<DonorSupporter> CreateDonorSupporterAsync(User User);
    }

}
