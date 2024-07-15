using BringMeBack.Data;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Users.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BringMeBackAPI.Services.Users.Services
{
    public class UserService : IUserservice
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users.Where(u => u.Role == role).ToListAsync();
        }


        public async Task<User> CreateUserAsync(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<User> UpdateUserAsync(int id, User User)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return null;

            existingUser.Name = User.Name;
            existingUser.Email = User.Email;
            existingUser.Password = User.Password;
            existingUser.PhoneNumber = User.PhoneNumber;
            existingUser.Location = User.Location;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
                return false;

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    public class OrganizationUserService : IOrganizationUserService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrganizationUser> CreateOrganizationUserAsync(User User)
        {
            var organizationUser = new OrganizationUser
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                OrganizationName = (User as OrganizationUser)?.OrganizationName,
                OrganizationType = (User as OrganizationUser)?.OrganizationType,
                RegistrationNumber = (User as OrganizationUser)?.RegistrationNumber,
                Address = (User as OrganizationUser)?.Address,
                ContactPerson = (User as OrganizationUser)?.ContactPerson,
                ContactPhoneNumber = (User as OrganizationUser)?.ContactPhoneNumber,
                ContactEmail = (User as OrganizationUser)?.ContactEmail
            };
            _context.OrganizationUsers.Add(organizationUser);
            await _context.SaveChangesAsync();
            return organizationUser;
        }
    }
    public class CommunityMemberService : ICommunityMemberService
    {
        private readonly ApplicationDbContext _context;

        public CommunityMemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommunityMember> CreateCommunityMemberAsync(User User)
        {
            var communityMember = new CommunityMember
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                CommunityRole = (User as CommunityMember)?.CommunityRole,
                CommunityAffiliation = (User as CommunityMember)?.CommunityAffiliation,
                Verification = (User as CommunityMember)?.Verification ?? false
            };
            _context.CommunityMembers.Add(communityMember);
            await _context.SaveChangesAsync();
            return communityMember;
        }
    }
    public class FamilyMemberService : IFamilyMemberService
    {
        private readonly ApplicationDbContext _context;

        public FamilyMemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FamilyMember> CreateFamilyMemberAsync(User User)
        {
            var familyMember = new FamilyMember
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                RelationToMissingPerson = (User as FamilyMember)?.RelationToMissingPerson,
                DetailsOfMissingPerson = (User as FamilyMember)?.DetailsOfMissingPerson,
                UploadPhoto = (User as FamilyMember)?.UploadPhoto
            };
            _context.FamilyMembers.Add(familyMember);
            await _context.SaveChangesAsync();
            return familyMember;
        }
    }
    public class PublicAuthorityService : IPublicAuthorityService
    {
        private readonly ApplicationDbContext _context;

        public PublicAuthorityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PublicAuthority> CreatePublicAuthorityAsync(User User)
        {
            var publicAuthority = new PublicAuthority
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                PositionOrAgency = (User as PublicAuthority)?.PositionOrAgency,
                Authorization = (User as PublicAuthority)?.Authorization ?? false,
                AccessCredentials = (User as PublicAuthority)?.AccessCredentials
            };
            _context.PublicAuthorities.Add(publicAuthority);
            await _context.SaveChangesAsync();
            return publicAuthority;
        }
    }
    public class VolunteerService : IVolunteerService
    {
        private readonly ApplicationDbContext _context;

        public VolunteerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Volunteer> CreateVolunteerAsync(User User)
        {
            var volunteer = new Volunteer
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                VolunteerExperience = (User as Volunteer)?.VolunteerExperience,
                Availability = (User as Volunteer)?.Availability,
                InterestArea = (User as Volunteer)?.InterestArea
            };
            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
            return volunteer;
        }
    }
    public class DonorSupporterService : IDonorSupporterService
    {
        private readonly ApplicationDbContext _context;

        public DonorSupporterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DonorSupporter> CreateDonorSupporterAsync(User User)
        {
            var donorSupporter = new DonorSupporter
            {
                Name = User.Name,
                Email = User.Email,
                Password = User.Password,
                PhoneNumber = User.PhoneNumber,
                Location = User.Location,
                Role = User.Role,
                DonationPreference = (User as DonorSupporter)?.DonationPreference,
                MessageOfSupport = (User as DonorSupporter)?.MessageOfSupport,
                PaymentInformation = (User as DonorSupporter)?.PaymentInformation
            };
            _context.DonorSupporters.Add(donorSupporter);
            await _context.SaveChangesAsync();
            return donorSupporter;
        }
    }


}
