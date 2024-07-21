using BringMeBackAPI.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace BringMeBackAPI.Models.Notifications
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string Message { get; set; }

        [Required]
        public bool IsRead { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsArchived { get; set; }
    }

}
