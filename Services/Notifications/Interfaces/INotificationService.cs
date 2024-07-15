using BringMeBackAPI.Models.Notifications;

namespace BringMeBackAPI.Services.Notifications.Interfaces
{

    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(int userId, string message);
        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(int userId);
        Task<bool> MarkAsReadAsync(int id);
    }
}
