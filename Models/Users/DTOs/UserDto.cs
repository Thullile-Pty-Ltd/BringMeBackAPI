﻿namespace BringMeBackAPI.Models.Users.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
