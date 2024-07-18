namespace BringMeBackAPI.Models.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Include Password in UserDto
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public UserRole Role { get; set; }

    }
}
