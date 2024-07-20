using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BringMeBackAPI.Models.Comments;
using BringMeBackAPI.Models.Payments;
using BringMeBackAPI.Models.Reports;
using BringMeBackAPI.Models.Users;

namespace BringMeBackAPI.Models.Reports.DTOs
{

    public class CreateReportUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public UserRole Role { get; set; }
        public string Token { get; set; }    

    }
}
