using Microsoft.AspNetCore.Mvc;
using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Services.Users.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using System.Security.Cryptography;
using BringMeBackAPI.Models.Users.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace BringMeBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (registerDto.Role == "GeneralUser")
            {
                // Clear validation errors for non-GeneralUser fields
                ModelState.Remove("Volunteer");
                ModelState.Remove("FamilyMember");
                ModelState.Remove("DonorSupporter");
                ModelState.Remove("CommunityMember");
                ModelState.Remove("PublicAuthority");
                ModelState.Remove("OrganizationUser");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert Role from string to UserRole enum
            if (!Enum.TryParse<UserRole>(registerDto.Role, out var userRole))
            {
                return BadRequest("Invalid role.");
            }

            User user;

            switch (userRole)
            {
                case UserRole.CommunityMember:
                    user = new CommunityMember
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.CommunityMember,
                        CommunityRole = registerDto.CommunityMember.CommunityRole,
                        CommunityAffiliation = registerDto.CommunityMember.CommunityAffiliation,
                        Verification = registerDto.CommunityMember.Verification
                    };
                    break;

                case UserRole.DonorSupporter:
                    user = new DonorSupporter
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.DonorSupporter,
                        DonationPreference = registerDto.DonorSupporter.DonationPreference,
                        MessageOfSupport = registerDto.DonorSupporter.MessageOfSupport,
                        PaymentInformation = registerDto.DonorSupporter.PaymentInformation
                    };
                    break;

                case UserRole.FamilyMember:
                    user = new FamilyMember
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.FamilyMember,
                        RelationToMissingPerson = registerDto.FamilyMember.RelationToMissingPerson,
                        DetailsOfMissingPerson = registerDto.FamilyMember.DetailsOfMissingPerson,
                        UploadPhoto = registerDto.FamilyMember.UploadPhoto
                    };
                    break;

                case UserRole.Organization:
                    user = new OrganizationUser
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.Organization,
                        OrganizationName = registerDto.OrganizationUser.OrganizationName,
                        OrganizationType = registerDto.OrganizationUser.OrganizationType,
                        RegistrationNumber = registerDto.OrganizationUser.RegistrationNumber,
                        Address = registerDto.OrganizationUser.Address,
                        ContactPerson = registerDto.OrganizationUser.ContactPerson,
                        ContactPhoneNumber = registerDto.OrganizationUser.ContactPhoneNumber,
                        ContactEmail = registerDto.OrganizationUser.ContactEmail
                    };
                    break;

                case UserRole.PublicAuthority:
                    user = new PublicAuthority
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.PublicAuthority,
                        PositionOrAgency = registerDto.PublicAuthority.PositionOrAgency,
                        Authorization = registerDto.PublicAuthority.Authorization,
                        AccessCredentials = registerDto.PublicAuthority.AccessCredentials
                    };
                    break;

                case UserRole.Volunteer:
                    user = new Volunteer
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.Volunteer,
                        VolunteerExperience = registerDto.Volunteer.VolunteerExperience,
                        Availability = registerDto.Volunteer.Availability,
                        InterestArea = registerDto.Volunteer.InterestArea
                    };
                    break;

                case UserRole.General:
                    user = new User
                    {
                        Name = registerDto.Name,
                        Email = registerDto.Email,
                        Password = registerDto.Password, // Pass the plain text password
                        PhoneNumber = registerDto.PhoneNumber,
                        Location = registerDto.Location,
                        Role = UserRole.General
                    };
                    break;

                default:
                    return BadRequest("Invalid user role.");
            }

            try
            {
                var registeredUser = await _userService.RegisterAsync(user);

                // Map to UserDto
                var userDto = new UserDto
                {
                    Id = registeredUser.Id,
                    Name = registeredUser.Name,
                    Email = registeredUser.Email,
                    Role = registeredUser.Role.ToString()
                };

                return CreatedAtAction(nameof(GetUserById), new { id = userDto.Id }, userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Authenticate user
                var user = await _userService.AuthenticateAsync(request.Email, request.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                // Generate JWT token
                var token = GenerateJWTToken(user);

                // Map to UserDto
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role.ToString(), // or .ToString() based on your requirement
                    Token = token // Add token to DTO
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateJWTToken(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

            var secretKey = _configuration["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1), // Consider a shorter expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }

}
