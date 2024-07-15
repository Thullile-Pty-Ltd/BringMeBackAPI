using BringMeBackAPI.Models.Notifications;
using BringMeBackAPI.Services.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> CreateNotification(int userId, string message)
        {
            var notification = await _notificationService.CreateNotificationAsync(userId, message);
            return CreatedAtAction(nameof(GetNotificationsByUserId), new { userId = notification.UserId }, notification);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByUserId(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var success = await _notificationService.MarkAsReadAsync(id);
            if (!success)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }

}
