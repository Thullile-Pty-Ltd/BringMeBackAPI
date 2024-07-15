using Microsoft.AspNetCore.Mvc;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Users.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BringMeBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserservice _Userservice;
        private readonly IOrganizationUserService _organizationUserService;
        private readonly ICommunityMemberService _communityMemberService;
        private readonly IFamilyMemberService _familyMemberService;
        private readonly IPublicAuthorityService _publicAuthorityService;
        private readonly IVolunteerService _volunteerService;
        private readonly IDonorSupporterService _donorSupporterService;

        public UsersController(
            IUserservice Userservice,
            IOrganizationUserService organizationUserService,
            ICommunityMemberService communityMemberService,
            IFamilyMemberService familyMemberService,
            IPublicAuthorityService publicAuthorityService,
            IVolunteerService volunteerService,
            IDonorSupporterService donorSupporterService)
        {
            _Userservice = Userservice;
            _organizationUserService = organizationUserService;
            _communityMemberService = communityMemberService;
            _familyMemberService = familyMemberService;
            _publicAuthorityService = publicAuthorityService;
            _volunteerService = volunteerService;
            _donorSupporterService = donorSupporterService;
        }

        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/GetUsers
        /// </remarks>
        /// <returns>A list of users.</returns>
        /// <response code="200">Returns the list of users.</response>
        /// <response code="500">If there was an internal server error.</response>
        [HttpGet("All", Name = "GetUsers")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _Userservice.GetAllUsersAsync());
        }

        /// <summary>
        /// Retrieves a specific user by unique id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/{id}
        /// </remarks>
        /// <param name="id">The id of the user to retrieve</param>
        /// <returns>The user with the specified id.</returns>
        /// <response code="200">Returns the user with the specified id.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _Userservice.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Retrieves users by their role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/users/role/{role}
        /// </remarks>
        /// <param name="role">The role of the users to retrieve</param>
        /// <returns>The list of users with the specified role.</returns>
        /// <response code="200">Returns the list of users with the specified role.</response>
        /// <response code="404">If no users with the specified role are found.</response>
        [HttpGet("role/{role}", Name = "GetUserByRole")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<User>>> GetUserByRole(UserRole role)
        {
            var users = await _Userservice.GetUsersByRoleAsync(role);

            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /api/User
        /// </remarks>
        /// <param name="user">The user to create</param>
        /// <returns>The created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If the user is null or invalid.</response>
        [HttpPost("Create", Name = "CreateUser")]
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            User createdUser = null;

            switch (user.Role)
            {
                case UserRole.Organization:
                    var organizationUser = new OrganizationUser
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role,
                        OrganizationName = (user as OrganizationUser)?.OrganizationName,
                        OrganizationType = (user as OrganizationUser)?.OrganizationType,
                        RegistrationNumber = (user as OrganizationUser)?.RegistrationNumber,
                        Address = (user as OrganizationUser)?.Address,
                        ContactPerson = (user as OrganizationUser)?.ContactPerson,
                        ContactPhoneNumber = (user as OrganizationUser)?.ContactPhoneNumber,
                        ContactEmail = (user as OrganizationUser)?.ContactEmail
                    };
                    createdUser = await _organizationUserService.CreateOrganizationUserAsync(organizationUser);
                    break;
                case UserRole.CommunityMember:
                    var communityMember = new CommunityMember
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role,
                        CommunityRole = (user as CommunityMember)?.CommunityRole,
                        CommunityAffiliation = (user as CommunityMember)?.CommunityAffiliation,
                        Verification = (user as CommunityMember)?.Verification ?? false
                    };
                    createdUser = await _communityMemberService.CreateCommunityMemberAsync(communityMember);
                    break;
                case UserRole.FamilyMember:
                    var familyMember = new FamilyMember
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role,
                        RelationToMissingPerson = (user as FamilyMember)?.RelationToMissingPerson,
                        DetailsOfMissingPerson = (user as FamilyMember)?.DetailsOfMissingPerson,
                        UploadPhoto = (user as FamilyMember)?.UploadPhoto
                    };
                    createdUser = await _familyMemberService.CreateFamilyMemberAsync(familyMember);
                    break;
                case UserRole.PublicAuthority:
                    var publicAuthority = new PublicAuthority
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role,
                        PositionOrAgency = (user as PublicAuthority)?.PositionOrAgency,
                        Authorization = (user as PublicAuthority)?.Authorization ?? false,
                        AccessCredentials = (user as PublicAuthority)?.AccessCredentials
                    };
                    createdUser = await _publicAuthorityService.CreatePublicAuthorityAsync(publicAuthority);
                    break;
                case UserRole.Volunteer:
                    var volunteer = new Volunteer
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role
                    };
                    createdUser = await _volunteerService.CreateVolunteerAsync(volunteer);
                    break;
                case UserRole.DonorSupporter:
                    var donorSupporter = new DonorSupporter
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        Location = user.Location,
                        Role = user.Role,
                        DonationPreference = (user as DonorSupporter)?.DonationPreference,
                        MessageOfSupport = (user as DonorSupporter)?.MessageOfSupport,
                        PaymentInformation = (user as DonorSupporter)?.PaymentInformation
                    };
                    createdUser = await _donorSupporterService.CreateDonorSupporterAsync(donorSupporter);
                    break;
                default:
                    createdUser = await _Userservice.CreateUserAsync(user);
                    break;
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     PUT /api/Users/{id}
        /// </remarks>
        /// <param name="id">The id of the user to update</param>
        /// <param name="user">The updated user</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the user is successfully updated.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpPut("{id}", Name = "PutUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutUser(int id, [FromBody] User user)
        {
            var updatedUser = await _Userservice.UpdateUserAsync(id, user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific user by unique id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     DELETE /api/users/{id}
        /// </remarks>
        /// <param name="id">The id of the user to delete</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the user is successfully deleted.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _Userservice.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
